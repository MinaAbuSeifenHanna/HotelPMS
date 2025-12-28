using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PMS.Features.RoomServiceRequests.Application.DTOS;
using PMS.Features.RoomServiceRequests.Application.Services;
using PMS.Features.RoomServiceRequests.Domain.Enums;

namespace PMS.Features.RoomServiceRequests.Presentation
{
    [ApiController]
    [Route("api/[controller]")]
    public class HousekeepingController : ControllerBase
    {
        private readonly IHousekeepingService _service;
        public HousekeepingController(IHousekeepingService service) => _service = service;

        [HttpGet("dashboard")]
        public async Task<IActionResult> GetDashboard() => Ok(await _service.GetDashboardAsync());

        [HttpPost("request")]
        public async Task<IActionResult> CreateRequest([FromBody] CreateServiceRequestDto dto)
        {
            var result = await _service.CreateRequestAsync(dto);
            return result ? Ok("Request Created") : BadRequest("Room not found");
        }

        [HttpPatch("room/{roomId}/status")]
        public async Task<IActionResult> UpdateStatus(int roomId, [FromQuery] HousekeepingStatus status)
        {
            await _service.UpdateRoomStatusAsync(roomId, status);
            return Ok($"Room {roomId} is now {status}");
        }
    }
}
