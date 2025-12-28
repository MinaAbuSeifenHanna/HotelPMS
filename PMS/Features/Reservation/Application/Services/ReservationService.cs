using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PMS.Core.Domain.Interfaces;
using PMS.Features.Guests.Domain.Entities;
using PMS.Features.Reservations.Application.DTOS;
using PMS.Features.Reservations.Application.Services;
using PMS.Features.Reservations.Domain.Entities;
using PMS.Features.Reservations.Domain.Enums;
using PMS.Features.Reservations.Domain.IRepositories;
using PMS.Features.Rooms.Domain.Entities;
using PMS.Features.Rooms.Domain.Enums;
using PMS.Features.RoomServiceRequests.Domain.Enums;

namespace PMS.Features.Reservations.Application.Services
{
    public class ReservationService : IReservationService
    {
        private readonly IReservationRepository _reservationRepo; // التغيير هنا
        private readonly IRepository<Reservation> _resRepo;
        private readonly IRepository<Room> _roomRepo;
        private readonly IMapper _mapper;

        public ReservationService(IReservationRepository reservationRepo, IRepository<Room> roomRepo, IMapper mapper)
        {
            _reservationRepo = reservationRepo;
            _roomRepo = roomRepo;
            _mapper = mapper;
        }

        public async Task<int> CreateReservationAsync(AddReservationDto dto)
        {
            // 1. جلب الغرفة والتأكد من وجودها
            var room = await _roomRepo.GetByIdAsync(dto.RoomId);
            if (room == null) throw new Exception("Room not found.");

            // 2. التحقق من حالة الغرفة (Maintenance) والنظافة (Housekeeping)
            if (room.Status == RoomStatus.OutOfService || room.HousekeepingStatus == HousekeepingStatus.Maintenance)
                throw new Exception("Room is under maintenance and cannot be booked.");

            // لو الحجز "اليوم" لازم الغرفة تكون نظيفة
            if (dto.CheckInDate.Date == DateTime.Now.Date && room.HousekeepingStatus != HousekeepingStatus.Cleaned)
                throw new Exception("Room is not cleaned yet. Cannot check-in today.");

            // 3. التحقق من المواعيد
            if (dto.CheckInDate.Date < DateTime.Now.Date)
                throw new Exception("Check-in date cannot be in the past.");

            var totalDays = (dto.CheckOutDate.Date - dto.CheckInDate.Date).Days;
            if (totalDays <= 0)
                throw new Exception("Check-out date must be after check-in date.");

            // 4. التحقق من تضارب المواعيد (Overlap)
            // هنا بنستخدم الـ Repo لجلب الحجوزات الحالية للغرفة
            var existingReservations = await _resRepo.GetAllAsync();
            bool isRoomBusy = existingReservations.Any(r =>
                r.RoomId == dto.RoomId &&
                r.Status != ReservationStatus.Cancelled &&
                r.Status != ReservationStatus.CheckedOut &&
                dto.CheckInDate < r.CheckOutDate &&
                dto.CheckOutDate > r.CheckInDate
            );

            if (isRoomBusy)
                throw new Exception("Room is already booked for the selected dates.");

            // 5. التحقق من السعة (Capacity)
            ValidateRoomCapacity(room.Type, dto.NumberOfGuests);

            // 6. استخدام الـ AutoMapper لتحويل الـ DTO لـ Entity
            var reservation = _mapper.Map<Reservation>(dto);

            // حساب السعر الكلي
            reservation.TotalAmount = totalDays * room.PricePerNight;
            reservation.Status = ReservationStatus.Confirmed;

            // 7. تحديث حالة الغرفة
            if (dto.CheckInDate.Date <= DateTime.Now.Date)
            {
                room.Status = RoomStatus.Occupied;
                reservation.Status = ReservationStatus.CheckedIn;
            }
            else
            {
                room.Status = RoomStatus.Reserved;
            }

            // 8. الحفظ من خلال الـ Repository
            await _resRepo.AddAsync(reservation);
            await _resRepo.SaveChangesAsync();

            return reservation.Id;
        }

        public async Task<bool> ProcessCheckInByIdNumberAsync(string idNumber)
        {
            // جلب الحجز مع بيانات الغرفة والضيف (Eager Loading)
            var reservations = await _resRepo.GetAllAsync(); // في الـ Real-world بنستخدم Include في الـ Repo
            var reservation = reservations.FirstOrDefault(r =>
                r.Guest.IdNumber == idNumber && r.Status == ReservationStatus.Confirmed);

            if (reservation == null)
                throw new Exception("No confirmed booking found for this ID.");

            // منع الـ Check-in لو الغرفة مش نظيفة
            if (reservation.Room.HousekeepingStatus != HousekeepingStatus.Cleaned)
                throw new Exception("Room is still dirty. Please contact Housekeeping.");

            reservation.Status = ReservationStatus.CheckedIn;
            reservation.Room.Status = RoomStatus.Occupied;

            await _resRepo.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ProcessCheckOutByIdNumberAsync(string idNumber)
        {
            var reservations = await _resRepo.GetAllAsync();
            var reservation = reservations.FirstOrDefault(r =>
                r.Guest.IdNumber == idNumber && r.Status == ReservationStatus.CheckedIn);

            if (reservation == null)
                throw new Exception("No active resident found with this ID.");

            // التحديث الأساسي
            reservation.Status = ReservationStatus.CheckedOut;

            // الربط مع الـ Housekeeping (Conflict Prevention)
            reservation.Room.Status = RoomStatus.Cleaning;
            reservation.Room.HousekeepingStatus = HousekeepingStatus.NeedsCleaning;

            await _resRepo.SaveChangesAsync();
            return true;
        }

        public async Task<bool> CancelReservationByGuestIdAsync(string idNumber)
        {
            var reservations = await _resRepo.GetAllAsync();
            var reservation = reservations
                .Where(r => r.Guest.IdNumber == idNumber && r.Status == ReservationStatus.Confirmed)
                .OrderBy(r => r.CheckInDate)
                .FirstOrDefault();

            if (reservation == null)
                throw new Exception("No active reservation found.");

            reservation.Status = ReservationStatus.Cancelled;

            // لو الغرفة كانت محجوزة (Reserved) ومفيش حجز تاني، نرجعها متاحة
            if (reservation.Room.Status == RoomStatus.Reserved)
            {
                reservation.Room.Status = RoomStatus.Available;
            }

            await _resRepo.SaveChangesAsync();
            return true;
        }

        private void ValidateRoomCapacity(RoomType type, int guestsCount)
        {
            switch (type)
            {
                case RoomType.Single when guestsCount > 1:
                    throw new Exception("Single room is for 1 guest only.");
                case RoomType.Double when guestsCount > 2:
                    throw new Exception("Double room is for 2 guests maximum.");
                case RoomType.Suite when guestsCount > 5:
                    throw new Exception("Suite is for 5 guests maximum.");
            }
        }

        // داخل ReservationService.cs
        public async Task<IEnumerable<ReservationResultDto>> GetAllReservationsAsync()
        {
            // بننده على الـ Repo المتخصص مش الـ Generic
            // ملاحظة: لازم تتأكد إنك عامل Inject لـ IReservationRepository في الـ Constructor
            var reservations = await _reservationRepo.GetAllWithDetailsAsync();

            return _mapper.Map<IEnumerable<ReservationResultDto>>(reservations);
        }
    }
}