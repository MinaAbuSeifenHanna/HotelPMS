using Microsoft.AspNetCore.Mvc;
using PMS.Features.Reservations.Application.DTOS;
using PMS.Features.Reservations.Application.Services;

namespace PMS.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReservationsController : ControllerBase
    {
        private readonly IReservationService _reservationService;

        public ReservationsController(IReservationService reservationService)
        {
            _reservationService = reservationService;
        }

        /// <summary>
        /// إنشاء حجز جديد (Booking)
        /// </summary>
        [HttpPost("create")]
        public async Task<IActionResult> CreateReservation([FromBody] AddReservationDto dto)
        {
            try
            {
                var reservationId = await _reservationService.CreateReservationAsync(dto);
                return Ok(new
                {
                    Message = "Reservation created successfully",
                    ReservationId = reservationId
                });
            }
            catch (Exception ex)
            {
                // الـ Service هترمي Exception لو الغرفة مش نظيفة أو محجوزة
                return BadRequest(new { Message = ex.Message });
            }
        }

        /// <summary>
        /// عملية التسكين (Check-In) بناءً على رقم الهوية
        /// </summary>
        [HttpPatch("check-in/{idNumber}")]
        public async Task<IActionResult> CheckIn(string idNumber)
        {
            try
            {
                await _reservationService.ProcessCheckInByIdNumberAsync(idNumber);
                return Ok(new { Message = "Check-in successful. Enjoy your stay!" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        /// <summary>
        /// عملية المغادرة (Check-Out) وتحويل الغرفة للتنظيف
        /// </summary>
        [HttpPatch("check-out/{idNumber}")]
        public async Task<IActionResult> CheckOut(string idNumber)
        {
            try
            {
                await _reservationService.ProcessCheckOutByIdNumberAsync(idNumber);
                return Ok(new { Message = "Check-out successful. Room sent to Housekeeping." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        /// <summary>
        /// إلغاء الحجز المؤكد
        /// </summary>
        [HttpPost("cancel/{idNumber}")]
        public async Task<IActionResult> CancelReservation(string idNumber)
        {
            try
            {
                var result = await _reservationService.CancelReservationByGuestIdAsync(idNumber);
                return Ok(new { Message = "Reservation cancelled successfully." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        [HttpGet("all-with-details")]
        public async Task<ActionResult<IEnumerable<ReservationResultDto>>> GetAllWithDetails()
        {
            try
            {
                var result = await _reservationService.GetAllReservationsAsync();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Error fetching reservations", error = ex.Message });
            }
        }
    }
}