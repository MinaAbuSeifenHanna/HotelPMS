

using Microsoft.AspNetCore.Mvc;
using PMS.Application.Features.Guests.DTOs;
using PMS.Application.Services;

namespace PMS.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class GuestsController : ControllerBase
    {
        private readonly IGuestService _guestService;

        public GuestsController(IGuestService guestService)
        {
            _guestService = guestService;
        }

        [HttpPost]
        public async Task<IActionResult> AddNewGuest([FromBody] AddGuestDto dto)
        {
            var result = await _guestService.AddGuestAsync(dto);
            if (!result.IsSuccess) return BadRequest(result.Message);
            return Ok(new { id = result.Data, result.Message });
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] string? search = null, [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            var result = await _guestService.GetAllGuestsAsync(search, pageNumber, pageSize);
            if (!result.IsSuccess) return BadRequest(result.Message);
            return Ok(new { result.Data, result.Message });
        }

        [HttpGet("{idNumber}")]
        public async Task<IActionResult> GetById(string idNumber)
        {
            var result = await _guestService.GetGuestByNumberIdAsync(idNumber);
            if (!result.IsSuccess) return BadRequest(result.Message);
            return Ok(result.Data);
        }

        [HttpPut("{idNumber}")]
        public async Task<IActionResult> Update(string idNumber, [FromBody] UpdateGuestDto dto)
        {
            var result = await _guestService.UpdateGuestAsync(idNumber, dto);
            if (!result.IsSuccess) return BadRequest(result.Message);
            return Ok(result.Message);
        }

        [HttpDelete("{idNumber}")]
        public async Task<IActionResult> Delete(string idNumber)
        {
            var result = await _guestService.DeleteGuestAsync(idNumber);
            if (!result.IsSuccess) return NotFound(result.Message);
            return Ok(new { message = "The guest's data has been permanently deleted." });
        }


        // endpoint to get vip guests

        //[HttpGet("vips")]
        //public async Task<IActionResult> GetVips()
        //{
        //    var vips = await _guestService.GetVipGuestsAsync();

        //    if (!vips.Any())
        //        return Ok(new { message = "There are currently no VIP guests registered." });

        //    return Ok(vips);
        //}

    }
}
