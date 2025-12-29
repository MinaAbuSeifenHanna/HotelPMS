using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PMS.Features.SPA.SpaRooms.Application.DTOS;
using PMS.Features.SPA.SpaRooms.Application.Services;

namespace PMS.Features.SPA.SpaRooms.Presentation
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class SpaRoomsController : ControllerBase
    {
        private readonly ISpaRoomService _spaRoomService;

        public SpaRoomsController(ISpaRoomService spaRoomService)
        {
            _spaRoomService = spaRoomService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var rooms = await _spaRoomService.GetAllAsync();
            return Ok(rooms);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var room = await _spaRoomService.GetByIdAsync(id);
            if (room == null)
                return NotFound(new { Message = "لم يتم العثور على غرفة السبا" });

            return Ok(room);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateSpaRoomDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var id = await _spaRoomService.CreateAsync(dto);

            return CreatedAtAction(
                nameof(GetById),
                new { id },
                new { Message = "تم إنشاء غرفة السبا بنجاح", Id = id }
            );
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateSpaRoomDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var updated = await _spaRoomService.UpdateAsync(id, dto);
            if (!updated)
                return NotFound(new { Message = "لم يتم العثور على غرفة السبا" });

            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _spaRoomService.DeleteAsync(id);
            if (!deleted)
                return NotFound(new { Message = "لم يتم العثور على غرفة السبا" });

            return Ok(new { Message = "تم تعطيل غرفة السبا بنجاح" });
        }
    }
}