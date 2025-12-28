using PMS.Features.Rooms.Domain.Entities;
using PMS.Features.RoomServiceRequests.Domain.Entities;
using PMS.Features.RoomServiceRequests.Domain.Enums;

namespace PMS.Features.RoomServiceRequests.Domain.IRepositories
{
    public interface IHousekeepingRepository
    {

        Task<Room> GetRoomByIdAsync(int roomId);


        Task<IEnumerable<Room>> GetAllRoomsWithStatusAsync();

        Task AddServiceRequestAsync(RoomServiceRequest request);


        Task UpdateRoomStatusAsync(int roomId, HousekeepingStatus status);
    }
}
