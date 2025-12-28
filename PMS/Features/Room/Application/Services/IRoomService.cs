using PMS.Features.Rooms.Application.DTOS;
using PMS.Features.Rooms.Domain.Enums;

namespace PMS.Features.Rooms.Application.Services
{
    public interface IRoomService
    {

        // add room - delete room
        Task<int> AddRoomAsync(AddRoomDto dto);
        Task<bool> DeleteRoomByNumberAsync(string roomNumber);

        // 3: change room status
        Task<bool> ChangeRoomStatusAsync(string roomNumber, RoomStatus newStatus);

        // 4-8: get room counts by status
        Task<RoomStatisticsDto> GetRoomsStatisticsAsync();
        // 9 & 10: search rooms by number and type
        Task<IEnumerable<RoomResultDto>> SearchRoomsAsync(string? roomNumber, RoomType? type);

      
    }
}
