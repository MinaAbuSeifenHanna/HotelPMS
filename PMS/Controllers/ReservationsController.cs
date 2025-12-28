//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using PMS.Features.Reservations.Application.DTOS;
//using PMS.Services;

//namespace PMS.Controllers
//{
//    [ApiController]
//    [Route("api/[controller]")]
//    public class ReservationsController : ControllerBase
//    {
//        private readonly IReservationService _reservationService;

//        public ReservationsController(IReservationService reservationService)
//        {
//            _reservationService = reservationService;
//        }

       
//        [HttpPost("create")]
//        public async Task<IActionResult> CreateReservation([FromBody] AddReservationDto dto)
//        {
//            try
//            {
//                var reservationId = await _reservationService.CreateReservationAsync(dto);
//                return Ok(new { Message = "Reservation created successfully", Id = reservationId });
//            }
//            catch (Exception ex)
//            {
//                return BadRequest(new { Message = ex.Message });
//            }
//        }

      
//        [HttpPatch("check-in/{idNumber}")]
//        public async Task<IActionResult> CheckIn(string idNumber)
//        {
//            try
//            {
//                await _reservationService.ProcessCheckInByIdNumberAsync(idNumber);
//                return Ok(new { Message = "Check-in successful. Room status updated to Occupied." });
//            }
//            catch (Exception ex)
//            {
//                return BadRequest(new { Message = ex.Message });
//            }
//        }

//        [HttpPatch("check-out/{idNumber}")]
//        public async Task<IActionResult> CheckOut(string idNumber)
//        {
//            try
//            {
//                await _reservationService.ProcessCheckOutByIdNumberAsync(idNumber);
//                return Ok(new { Message = "Check-out successful. Room status updated to Cleaning." });
//            }
//            catch (Exception ex)
//            {
//                return BadRequest(new { Message = ex.Message });
//            }
//        }


//        [HttpPost("cancel/{idNumber}")]
//        public async Task<IActionResult> CancelReservation(string idNumber)
//        {
//            if (string.IsNullOrWhiteSpace(idNumber))
//                return BadRequest(new { success = false, message = "IdNumber is required in the URL." });

//            try
//            {
//                var result = await _reservationService.CancelReservationByGuestIdAsync(idNumber);
//                return Ok(new { success = result, message = "Reservation cancelled successfully." });
//            }
//            catch (Exception ex)
//            {
//                return BadRequest(new { success = false, message = ex.Message });
//            }
//        }


//    }
//}
