using Microsoft.EntityFrameworkCore;
using PMS.Domain.entity;
using PMS.Domain.enums;
using PMS.DTOs;
using PMS.Services;

public class ReservationService : IReservationService
{
    private readonly PMSContext _context;

    public ReservationService(PMSContext context)
    {
        _context = context;
    }

    public async Task<string> CheckInAsync(CheckInDto dto)
    {
        // 1. get room by room number

        var room = await _context.Rooms
            .FirstOrDefaultAsync(r => r.RoomNumber == dto.RoomNumber);

        // 2. check if room exists and is available

        if (room == null)
            return "Error: Room number not found in the system";

        if (room.Status != RoomStatus.Available)
            return $"Room  {dto.RoomNumber} Its status is currently {room.Status} It cannot be accommodated for guests.";


        // 3. Is the guest listed or not?

        var guest = await _context.Guests
            .FirstOrDefaultAsync(g => g.NationalId == dto.GuestNationalId);

        if (guest == null)
        {
            guest = new Guest
            {
                FullName = dto.GuestName,
                NationalId = dto.GuestNationalId,
                Phone = dto.GuestPhone
            };
            _context.Guests.Add(guest);
            await _context.SaveChangesAsync();
        }

        // 4. store in Reservation Table

        var reservation = new Reservation
        {
            RoomId = room.Id, 
            GuestId = guest.Id,
            CheckInDate = DateTime.Now,
            Status = ReservationStatus.CheckedIn,
            IsCheckedOut = false
        };

        // 5. updata Room Status

        room.Status = RoomStatus.Occupied;

        _context.Reservations.Add(reservation);
        await _context.SaveChangesAsync();

        return $"successfully Checked In to Room {room.RoomNumber}";
    }


    public async Task<string> CheckOutAsync(string roomNumber)
    {
        // 1. search for the room by room number
        var room = await _context.Rooms.FirstOrDefaultAsync(r => r.RoomNumber == roomNumber);
        if (room == null) return "The room number is incorrect";

        // 2. check for active reservation

        var reservation = await _context.Reservations
            .Include(r => r.Guest)
            .Include(r => r.Room)
            .FirstOrDefaultAsync(r => r.RoomId == room.Id && r.IsCheckedOut == false);

        if (reservation == null)
            return $"There is currently no active booking for room number {roomNumber}.";

        // 3.  calculate the invoice

        var checkOutTime = DateTime.Now;
        var stayDuration = (int)Math.Ceiling((checkOutTime - reservation.CheckInDate).TotalDays);
        if (stayDuration <= 0) stayDuration = 1;

        decimal totalInvoice = stayDuration * room.PricePerNight;

        // 4. close the reservation

        reservation.CheckOutDate = checkOutTime;
        reservation.Status = ReservationStatus.CheckedOut;
        reservation.IsCheckedOut = true;
        reservation.TotalAmount = totalInvoice;

        // 5. room status to dirty
        room.Status = RoomStatus.Dirty;

        await _context.SaveChangesAsync();

        return $" Check-out from the room {roomNumber} Successfully ,  guest : {reservation.Guest?.FullName} Accommodation : {stayDuration} day , The account : {totalInvoice} $.";
    }


}