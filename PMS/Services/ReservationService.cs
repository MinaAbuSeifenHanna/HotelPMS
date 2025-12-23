using Microsoft.EntityFrameworkCore;
using PMS.Domain.entity;
using PMS.Domain.enums;
using PMS.Domain.enums.RoomEnums;
using PMS.DTOs;
using PMS.DTOs.Reservation;
using PMS.Services;

public class ReservationService : IReservationService
{
    private readonly PMSContext _context;

    public ReservationService(PMSContext context)
    {
        _context = context;
    }

    public async Task<int> CreateReservationAsync(AddReservationDto dto)
    {
        // 1. check if room exists
        var room = await _context.Rooms.FindAsync(dto.RoomId);
        if (room == null) throw new Exception("The room does not exist");

        if (dto.CheckInDate.Date < DateTime.Now.Date)
            throw new Exception("Reservations cannot be made for an older date.");


        // 2. calculate number of days between check-in and check-out

        var totalDays = (dto.CheckOutDate.Date - dto.CheckInDate.Date).Days;
        if (totalDays <= 0) throw new Exception("The exit date must be after the entry date.");

        bool isRoomBusy = await _context.Reservations.AnyAsync(r =>
       r.RoomId == dto.RoomId &&
       r.Status != ReservationStatus.Cancelled && 
       r.Status != ReservationStatus.CheckedOut && 
       dto.CheckInDate < r.CheckOutDate && 
       dto.CheckOutDate > r.CheckInDate); 

        if (isRoomBusy)
            throw new Exception("Sorry, this room is already booked for the selected period.");



        // 3. calculate total amount
        var totalAmount = totalDays * room.PricePerNight;


        // 4. create reservation entity
        var reservation = new Reservation
        {
            GuestId = dto.GuestId,
            RoomId = dto.RoomId,
            CheckInDate = dto.CheckInDate,
            CheckOutDate = dto.CheckOutDate,
            NumberOfGuests = dto.NumberOfGuests,
            TotalAmount = totalAmount,
            DepositAmount = dto.DepositAmount,
            PaymentMethod = dto.PaymentMethod,
            BookingSource = dto.BookingSource,
            SpecialRequests = dto.SpecialRequests,
            Status = ReservationStatus.Confirmed,

            //  Companions
            Companions = dto.Companions?.Select(c => new Companion
            {
                FirstName = c.FirstName,
                LastName = c.LastName,
                Relationship = c.Relationship,
                Age = c.Age
            }).ToList() ?? new List<Companion>()
        };

        // 5. change room status to Reserved
        room.Status = RoomStatus.Reserved;

        _context.Reservations.Add(reservation);
        await _context.SaveChangesAsync();

        return reservation.Id;
    }

    
    public async Task<bool> ProcessCheckInByIdNumberAsync(string idNumber)
    {
        
        var reservation = await _context.Reservations
            .Include(r => r.Guest)
            .Include(r => r.Room)
            .FirstOrDefaultAsync(r => r.Guest.IdNumber == idNumber && r.Status == ReservationStatus.Confirmed);

        if (reservation == null)
            throw new Exception("There is no confirmed booking ready for accommodation with this national ID number.");

        
        reservation.Status = ReservationStatus.CheckedIn;
        reservation.Room.Status = RoomStatus.Occupied;

        await _context.SaveChangesAsync();
        return true;
    }


    public async Task<bool> ProcessCheckOutByIdNumberAsync(string idNumber)
    {
  
        var reservation = await _context.Reservations
            .Include(r => r.Guest)
            .Include(r => r.Room)
            .FirstOrDefaultAsync(r => r.Guest.IdNumber == idNumber && r.Status == ReservationStatus.CheckedIn);

        if (reservation == null)
            throw new Exception("There is currently no resident with this national ID number.");

        reservation.Status = ReservationStatus.CheckedOut;
        reservation.Room.Status = RoomStatus.Cleaning;

        await _context.SaveChangesAsync();
        return true;
    }

}