using Microsoft.EntityFrameworkCore;
using PMS.Core.Infrastructure.RepoIMP;
using PMS.Features.Rooms.Domain.Entities;
using PMS.Features.Rooms.Domain.Enums;
using PMS.Features.Rooms.Domain.IRepositories;

namespace PMS.Features.Rooms.Infrastructure.RepositoriesIMP
{
    public class RoomRepository : Repository<Room>, IRoomRepository
    {
        public RoomRepository(PMSContext context) : base(context) { }

        public async Task<Room?> GetRoomByNumberAsync(string roomNumber)
        {
            return await _dbSet
                .Include(r => r.Reservations) 
                .FirstOrDefaultAsync(r => r.RoomNumber == roomNumber);
        }

        public async Task<bool> ExistsAsync(string roomNumber)
            => await _dbSet.AnyAsync(r => r.RoomNumber == roomNumber);

        public async Task<IEnumerable<Room>> SearchRoomsAsync(string? roomNumber, RoomType? type)
        {
            var query = _dbSet.AsQueryable();

            if (!string.IsNullOrEmpty(roomNumber))
                query = query.Where(r => r.RoomNumber.Contains(roomNumber));

            if (type.HasValue)
                query = query.Where(r => r.Type == type.Value);

            return await query.ToListAsync();
        }
  
    }
}
