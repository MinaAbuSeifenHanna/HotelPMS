using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PMS.Features.Guests.Application.DTOS;
using PMS.Features.Guests.Application.Services;

namespace PMS.Features.Guests.Presentation
{
    [ApiController]
    [Route("api/[controller]")]
    public class GuestsController : ControllerBase
    {
        private readonly IGuestService _guestService;

        public GuestsController(IGuestService guestService) => _guestService = guestService;

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] AddGuestDto dto)
        {
            var id = await _guestService.AddGuestAsync(dto);
            return CreatedAtAction(nameof(GetById), new { idNumber = dto.IdNumber }, new { Id = id });
        }

        [HttpGet]
        public async Task<IActionResult> GetAll() => Ok(await _guestService.GetAllGuestsAsync());

        [HttpGet("{idNumber}")]
        public async Task<IActionResult> GetById(string idNumber)
        {
            var guest = await _guestService.GetGuestByIdAsync(idNumber);
            return guest != null ? Ok(guest) : NotFound("الضيف غير موجود");
        }

        [HttpPut("{idNumber}")]
        public async Task<IActionResult> Update(string idNumber, [FromBody] UpdateGuestDto dto)
        {
            var result = await _guestService.UpdateGuestAsync(idNumber, dto);
            return result ? Ok("تم التحديث بنجاح") : BadRequest("فشل التحديث");
        }

        [HttpDelete("{idNumber}")]
        public async Task<IActionResult> Delete(string idNumber)
        {
            await _guestService.DeleteGuestByIdNumberAsync(idNumber);
            return Ok("تم حذف الضيف بنجاح");
        }
    }
}
