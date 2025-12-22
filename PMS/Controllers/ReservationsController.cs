using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PMS.DTOs;
using PMS.Services;

namespace PMS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationsController : ControllerBase
    {

        private readonly IReservationService _resService;

        public ReservationsController(IReservationService resService)
        {
            _resService = resService;
        }

        [HttpPost("check-in")]
        public async Task<IActionResult> CheckIn([FromBody] CheckInDto dto)
        {
            var result = await _resService.CheckInAsync(dto);
            if (result.Contains("successfully"))
                return Ok(result);

            return BadRequest(result);
        }


        [HttpPut("check-out/{id}")]
        public async Task<IActionResult> CheckOut(string roomNumber)
        {
            var result = await _resService.CheckOutAsync(roomNumber);

            if (result.Contains("not found"))
                return NotFound(result);

            return Ok(result);
        }
    }
}
