using PMS.Features.RoomServiceRequests.Application.DTOS;
using PMS.Features.RoomServiceRequests.Domain.Enums;

namespace PMS.Features.RoomServiceRequests.Application.Services
{
    public interface IHousekeepingService
    {
        Task<HousekeepingDashboardDto> GetDashboardAsync();

        Task<bool> CreateRequestAsync(CreateServiceRequestDto dto);

        Task<bool> UpdateRoomStatusAsync(int roomId, HousekeepingStatus newStatus);
    }
}
