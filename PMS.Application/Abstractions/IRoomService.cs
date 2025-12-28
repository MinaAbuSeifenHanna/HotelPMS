
using PMS.Application.Features.Rooms;
using PMS.Domain.enums.RoomEnums;

namespace PMS.Application.Services
{
    public interface IRoomService
    {

        // add room - delete room
        Task<int> AddRoomAsync(AddRoomDto dto);
        Task<bool> DeleteRoomByNumberAsync(string roomNumber);

        // 3: change room status
        Task<bool> ChangeRoomStatusAsync(string roomNumber, RoomStatus newStatus);

        // 4-8: get room counts by status
        Task<int> GetRoomCountByStatusAsync(RoomStatus status);

        // 9 & 10: search rooms by number and type
        Task<IEnumerable<RoomResultDto>> SearchRoomsAsync(string? roomNumber, RoomType? type);


    }
}
