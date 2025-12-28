using PMS.Core.Domain.Interfaces;
using PMS.Features.Rooms.Domain.Entities;
using PMS.Features.Rooms.Domain.Enums;

namespace PMS.Features.Rooms.Domain.IRepositories
{
    public interface IRoomRepository : IRepository<Room>

    {
        Task<Room?> GetRoomByNumberAsync(string roomNumber);

        Task<bool> ExistsAsync(string roomNumber);
        Task<IEnumerable<Room>> SearchRoomsAsync(string? roomNumber, RoomType? type);
    
}
}
