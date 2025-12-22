using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PMS.Domain.entity;
using PMS.Domain.enums;
using PMS.DTOs;
using PMS.Services;

namespace PMS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomsController : ControllerBase
    {
        private readonly IRoomService _roomService;

        //  (Dependency Injection)
        public RoomsController(IRoomService roomService)
        {
            _roomService = roomService;
        }

        // 
        [HttpGet]
        public async Task<IActionResult> GetRooms()
        {
            var rooms = await _roomService.GetAllRoomsAsync();
            return Ok(rooms);
        }


        [HttpPost]
        public async Task<IActionResult> CreateRoom([FromBody] CreateRoomDto roomDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

          
            var room = new Room
            {
                RoomNumber = roomDto.RoomNumber,
                RoomType = roomDto.RoomType,
                PricePerNight = roomDto.PricePerNight,
                Status = RoomStatus.Available 
            };

            await _roomService.AddRoomAsync(room);
            return Ok("Room added successfully!");
        }


        [HttpPut("update-status/{roomNumber}")]
        public async Task<IActionResult> UpdateStatus(string roomNumber, [FromBody] RoomStatus newStatus)
        {
            var result = await _roomService.UpdateRoomStatusAsync(roomNumber, newStatus);
            if (!result) return NotFound("Room not found");

            return Ok("Status updated successfully");
        }
    }
}
