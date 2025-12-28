using Microsoft.EntityFrameworkCore;
using PMS.Features.Rooms.Domain.Entities;
using PMS.Features.Rooms.Domain.Enums;
using PMS.Features.RoomServiceRequests.Domain.Entities;
using PMS.Features.RoomServiceRequests.Domain.Enums;
using PMS.Features.RoomServiceRequests.Domain.IRepositories;

namespace PMS.Features.RoomServiceRequests.Infrastructure
{
    public class HousekeepingRepository : IHousekeepingRepository
    {
        private readonly PMSContext _context;

        public HousekeepingRepository(PMSContext context) => _context = context;

        public async Task<IEnumerable<Room>> GetAllRoomsWithStatusAsync()
        {
            return await _context.Rooms
                .Include(r => r.ServiceRequests)
                .ToListAsync();
        }

        public async Task<Room> GetRoomByIdAsync(int roomId)
        {
            return await _context.Rooms
                .Include(r => r.ServiceRequests)
                .FirstOrDefaultAsync(r => r.Id == roomId);
        }

        public async Task AddServiceRequestAsync(RoomServiceRequest request)
        {
            await _context.RoomServiceRequests.AddAsync(request);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateRoomStatusAsync(int roomId, HousekeepingStatus status)
        {
            var room = await _context.Rooms.FindAsync(roomId);
            if (room == null) throw new Exception("Room not found.");

            room.HousekeepingStatus = status;

            // الـ Logic العبقري للربط بين الاقسام
            room.Status = status switch
            {
                HousekeepingStatus.Cleaned => RoomStatus.Available,
                HousekeepingStatus.NeedsCleaning => RoomStatus.Cleaning,
                HousekeepingStatus.InProgress => RoomStatus.Cleaning,
                HousekeepingStatus.Maintenance => RoomStatus.OutOfService,
                _ => room.Status
            };

            await _context.SaveChangesAsync();
        }
    }
}