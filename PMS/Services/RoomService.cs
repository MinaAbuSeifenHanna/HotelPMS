using Microsoft.EntityFrameworkCore;
using PMS.Domain.entity;
using PMS.Domain.enums.RoomEnums;
using PMS.DTOs.Room;

namespace PMS.Services
{
    public class RoomService : IRoomService
    {
        private readonly PMSContext _context;
        public RoomService(PMSContext context) => _context = context;



        // 1. Add new room
        public async Task<int> AddRoomAsync(AddRoomDto dto)
        {
            //  check if room number already exists
            if (await _context.Rooms.AnyAsync(r => r.RoomNumber == dto.RoomNumber))
                throw new Exception("The room number is pre-registered.");

            var room = new Room
            {
                RoomNumber = dto.RoomNumber,
                Type = dto.Type,
                Floor = dto.Floor,
                PricePerNight = dto.PricePerNight,
                Status = dto.Status
            };

            _context.Rooms.Add(room);
            await _context.SaveChangesAsync();
            return room.Id;
        }

        // 2. delete room by room number
        public async Task<bool> DeleteRoomByNumberAsync(string roomNumber)
        {
            var room = await _context.Rooms
                .Include(r => r.Reservations)
                .FirstOrDefaultAsync(r => r.RoomNumber == roomNumber);

            if (room == null) return false;

            // check if room is linked to any reservations
            if (room.Reservations.Any())
                throw new Exception("A room associated with a reservation cannot be deleted.");

            _context.Rooms.Remove(room);
            await _context.SaveChangesAsync();
            return true;
        }

        // 3. change room status
        public async Task<bool> ChangeRoomStatusAsync(string roomNumber, RoomStatus newStatus)
        {
            var room = await _context.Rooms.FirstOrDefaultAsync(r => r.RoomNumber == roomNumber);
            if (room == null) return false;

            room.Status = newStatus;
            await _context.SaveChangesAsync();
            return true;
        }


        // 4: 8
        public async Task<int> GetRoomCountByStatusAsync(RoomStatus status)
        {
            return await _context.Rooms.CountAsync(r => r.Status == status);
        }
        // 9 & 10
        public async Task<IEnumerable<RoomResultDto>> SearchRoomsAsync(string? roomNumber, RoomType? type)
        {
            var query = _context.Rooms.AsQueryable();

            if (!string.IsNullOrEmpty(roomNumber))
                query = query.Where(r => r.RoomNumber.Contains(roomNumber));

            if (type.HasValue)
                query = query.Where(r => r.Type == type.Value);

            return await query.Select(r => new RoomResultDto
            {
                RoomNumber = r.RoomNumber,
                Type = r.Type.ToString(),
                Floor = r.Floor,
                Status = r.Status.ToString(),
                PricePerNight = r.PricePerNight
            }).ToListAsync();
        }


    }
}
