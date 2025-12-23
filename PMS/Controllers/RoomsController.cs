using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PMS.Domain.entity;
using PMS.Domain.enums.RoomEnums;
using PMS.DTOs;
using PMS.DTOs.Room;
using PMS.Services;

namespace PMS.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RoomsController : ControllerBase
    {
        private readonly IRoomService _roomService;

        public RoomsController(IRoomService roomService)
        {
            _roomService = roomService;
        }

        // 1- Add New Room
        [HttpPost("add")]
        public async Task<IActionResult> AddRoom([FromBody] AddRoomDto dto)
        {
            try
            {
                var roomId = await _roomService.AddRoomAsync(dto);
                return Ok(new { Message = "The room has been added successfully", RoomId = roomId });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // 2- Delete Room 
        [HttpDelete("delete/{roomNumber}")]
        public async Task<IActionResult> DeleteRoom(string roomNumber)
        {
            try
            {
                var result = await _roomService.DeleteRoomByNumberAsync(roomNumber);
                if (!result) return NotFound("The room does not exist.");
                return Ok(new { Message = "The room has been successfully deleted" });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // 3- Change Status
        [HttpPatch("change-status")]
        public async Task<IActionResult> ChangeStatus(string roomNumber, RoomStatus newStatus)
        {
            var result = await _roomService.ChangeRoomStatusAsync(roomNumber, newStatus);
            if (!result) return NotFound("The room does not exist");
            return Ok(new { Message = "The room status was successfully changed." });
        }

        // 4 to 8 - Get Counts 
        [HttpGet("statistics")]
        public async Task<IActionResult> GetRoomsStatistics()
        {
            var stats = new
            {
                Available = await _roomService.GetRoomCountByStatusAsync(RoomStatus.Available),
                Occupied = await _roomService.GetRoomCountByStatusAsync(RoomStatus.Occupied),
                Reserved = await _roomService.GetRoomCountByStatusAsync(RoomStatus.Reserved),
                Cleaning = await _roomService.GetRoomCountByStatusAsync(RoomStatus.Cleaning),
                Maintenance = await _roomService.GetRoomCountByStatusAsync(RoomStatus.Maintenance)
            };
            return Ok(stats);
        }

        // 9 & 10 - Search and Filter
        [HttpGet("search")]
        public async Task<IActionResult> SearchRooms([FromQuery] string? roomNumber, [FromQuery] RoomType? type)
        {
            var results = await _roomService.SearchRoomsAsync(roomNumber, type);
            return Ok(results);
        }
    }
}
