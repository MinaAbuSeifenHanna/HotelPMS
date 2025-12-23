using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PMS.DTOs.Guest;
using PMS.Services;

namespace PMS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GuestsController : ControllerBase
    {
        private readonly IGuestService _guestService;

        public GuestsController(IGuestService guestService)
        {
            _guestService = guestService;
        }

        [HttpPost("add-new-guest")]
        public async Task<IActionResult> AddNewGuest([FromBody] AddGuestDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var guestId = await _guestService.AddGuestAsync(dto);
            return Ok(new { Message = "The guest was added successfully.", GuestId = guestId });
        }


        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var guests = await _guestService.GetAllGuestsAsync();
            return Ok(guests);
        }

        [HttpGet("search/{idNumber}")]
        public async Task<IActionResult> GetById(string idNumber)
        {
            var guest = await _guestService.GetGuestByIdAsync(idNumber);
            if (guest == null) return NotFound("The guest is not present");
            return Ok(guest);
        }

        [HttpPut("update/{idNumber}")]
        public async Task<IActionResult> Update(String idNumber, [FromBody] UpdateGuestDto dto)
        {
            var result = await _guestService.UpdateGuestAsync(idNumber, dto);
            if (!result) return BadRequest("Data update failed");
            return Ok("Updated successfully");
        }

        // delete guest
        [HttpDelete("delete-by-idnumber/{idNumber}")]
        public async Task<IActionResult> Delete(string idNumber)
        {
            try
            {
                var result = await _guestService.DeleteGuestByIdNumberAsync(idNumber);
                if (!result) return NotFound($"No guest is registered with ID number: {idNumber}");

                return Ok(new { message = "The guest's data has been permanently deleted." });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
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
