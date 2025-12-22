using Microsoft.EntityFrameworkCore;
using PMS.Domain.entity;
using PMS.Domain.enums;

namespace PMS.Services
{
    public class RoomService : IRoomService
    {
        private readonly PMSContext _context;

        public RoomService(PMSContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Room>> GetAllRoomsAsync()
        {
            return await _context.Rooms.ToListAsync();
        }


        public async Task<bool> UpdateRoomStatusAsync(string roomNumber, RoomStatus newStatus)
        {
            var room = await _context.Rooms.FirstOrDefaultAsync(r => r.RoomNumber == roomNumber);

            if (room == null) return false;

            room.Status = newStatus;
            await _context.SaveChangesAsync();
            return true;
        }


        public async Task<bool> AddRoomAsync(Room room)
        {
            _context.Rooms.Add(room);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<Room> GetRoomByIdAsync(int id)
        {
            return await _context.Rooms.FindAsync(id);
        }
    }
}
