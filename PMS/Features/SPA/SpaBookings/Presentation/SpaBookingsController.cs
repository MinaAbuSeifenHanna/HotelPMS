using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PMS.Features.SPA.SpaBookings.Application.DTOS;
using PMS.Features.SPA.SpaBookings.Application.Services;

namespace PMS.Features.SPA.SpaBookings.Presentation
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class SpaBookingsController : ControllerBase
    {
          private readonly ISpaBookingService _spaBookingService;

        public SpaBookingsController(ISpaBookingService spaBookingService)
        {
            _spaBookingService = spaBookingService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(string ? search = null)
        {
            var bookings = await _spaBookingService.GetAllAsync(search);
            return Ok(bookings);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var booking = await _spaBookingService.GetByIdAsync(id);
            if (booking == null)
                return NotFound(new { Message = "لم يتم العثور على الحجز" });

            return Ok(booking);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateSpaBookingDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var id = await _spaBookingService.CreateAsync(dto);

            return CreatedAtAction(
                nameof(GetById),
                new { id },
                new { Message = "تم إنشاء الحجز بنجاح", Id = id }
            );
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateSpaBookingDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var updated = await _spaBookingService.UpdateAsync(id, dto);
            if (!updated)
                return NotFound(new { Message = "لم يتم العثور على الحجز" });

            return NoContent();
        }

        [HttpPatch("{id:int}/cancel")]
        public async Task<IActionResult> Cancel(int id)
        {
            var cancelled = await _spaBookingService.CancelAsync(id);
            if (!cancelled)
                return NotFound(new { Message = "لم يتم العثور على الحجز" });

            return NoContent();
        }
    }
    }
