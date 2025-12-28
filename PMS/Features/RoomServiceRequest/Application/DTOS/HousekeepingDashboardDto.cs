
namespace PMS.Features.RoomServiceRequests.Application.DTOS
{
    public class HousekeepingDashboardDto
    {
        public Dictionary<string, int> StatusCounts { get; set; }
        public List<RoomHousekeepingDto> Rooms { get; set; }
    }
}
