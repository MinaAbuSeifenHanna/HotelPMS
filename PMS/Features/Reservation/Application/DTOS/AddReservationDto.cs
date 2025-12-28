using PMS.Features.Reservations.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace PMS.Features.Reservations.Application.DTOS
{
    public class AddReservationDto
    {
        [Required]
        public int GuestId { get; set; }

        [Required]
        public int RoomId { get; set; }

        [Required]
        public DateTime CheckInDate { get; set; }

        [Required]
        public DateTime CheckOutDate { get; set; }

        public int NumberOfGuests { get; set; }

        public List<AddCompanionDto>? Companions { get; set; }

        public decimal DepositAmount { get; set; } 

        public PaymentMethod PaymentMethod { get; set; }
        public BookingSource BookingSource { get; set; }
        public string? SpecialRequests { get; set; }
    }

   
}
