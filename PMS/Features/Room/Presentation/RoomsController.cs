using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PMS.Features.Rooms.Application.DTOS;
using PMS.Features.Rooms.Application.Services;
using PMS.Features.Rooms.Domain.Enums;
namespace PMS.Features.Rooms.Presentation
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

        [HttpPost] 
        public async Task<IActionResult> AddRoom([FromBody] AddRoomDto dto)
        {
            var roomId = await _roomService.AddRoomAsync(dto);
            return CreatedAtAction(nameof(SearchRooms), new { roomNumber = dto.RoomNumber }, new { Message = "Room added", Id = roomId });
        }

        [HttpDelete("{roomNumber}")] 
        public async Task<IActionResult> DeleteRoom(string roomNumber)
        {
            var result = await _roomService.DeleteRoomByNumberAsync(roomNumber);
            if (!result) return NotFound(new { Message = "The room does not exist." });
            return Ok(new { Message = "The room has been successfully deleted" });
        }

        [HttpPatch("{roomNumber}/status")] 
        public async Task<IActionResult> ChangeStatus(string roomNumber, [FromBody] RoomStatus newStatus)
        {
            var result = await _roomService.ChangeRoomStatusAsync(roomNumber, newStatus);
            if (!result) return NotFound(new { Message = "The room does not exist" });
            return Ok(new { Message = "The room status was successfully changed." });
        }

        [HttpGet("statistics")]
        public async Task<IActionResult> GetRoomsStatistics()
        {
            return Ok(await _roomService.GetRoomsStatisticsAsync());
        }

        [HttpGet("search")]
        public async Task<IActionResult> SearchRooms([FromQuery] string? roomNumber, [FromQuery] RoomType? type)
        {
            return Ok(await _roomService.SearchRoomsAsync(roomNumber, type));
        }
    }

}



//    [ApiController]
//    [Route("api/[controller]")]
//    public class RoomsController : ControllerBase
//    {
//        private readonly IRoomService _roomService;

//        public RoomsController(IRoomService roomService)
//        {
//            _roomService = roomService;
//        }

//        // 1- Add New Room
//        [HttpPost("add")]
//        public async Task<IActionResult> AddRoom([FromBody] AddRoomDto dto)
//        {
//            try
//            {
//                var roomId = await _roomService.AddRoomAsync(dto);
//                return Ok(new { Message = "The room has been added successfully", RoomId = roomId });
//            }
//            catch (Exception ex)
//            {
//                return BadRequest(ex.Message);
//            }
//        }

//        // 2- Delete Room 
//        [HttpDelete("delete/{roomNumber}")]
//        public async Task<IActionResult> DeleteRoom(string roomNumber)
//        {
//            try
//            {
//                var result = await _roomService.DeleteRoomByNumberAsync(roomNumber);
//                if (!result) return NotFound("The room does not exist.");
//                return Ok(new { Message = "The room has been successfully deleted" });
//            }
//            catch (Exception ex)
//            {
//                return BadRequest(ex.Message);
//            }
//        }

//        // 3- Change Status
//        [HttpPatch("change-status")]
//        public async Task<IActionResult> ChangeStatus(string roomNumber, RoomStatus newStatus)
//        {
//            var result = await _roomService.ChangeRoomStatusAsync(roomNumber, newStatus);
//            if (!result) return NotFound("The room does not exist");
//            return Ok(new { Message = "The room status was successfully changed." });
//        }

//        // 4 to 8 - Get Counts 
//        [HttpGet("statistics")]
//        public async Task<IActionResult> GetRoomsStatistics()
//        {
//            var stats = await _roomService.GetRoomsStatisticsAsync();
//            return Ok(stats);
//        }


//        // 9 & 10 - Search and Filter
//        [HttpGet("search")]
//        public async Task<IActionResult> SearchRooms([FromQuery] string? roomNumber, [FromQuery] RoomType? type)
//        {
//            var results = await _roomService.SearchRoomsAsync(roomNumber, type);
//            return Ok(results);
//        }
//    }
//}
