using PMS.Domain.entity;
using PMS.Domain.enums;

namespace PMS.Services
{
    public interface IRoomService
    {
        Task<IEnumerable<Room>> GetAllRoomsAsync(); 
        Task<Room> GetRoomByIdAsync(int id);

        Task<bool> UpdateRoomStatusAsync(string roomNumber, RoomStatus newStatus); 
        Task<bool> AddRoomAsync(Room room); 
    }
}
