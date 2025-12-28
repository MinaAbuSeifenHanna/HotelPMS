using PMS.Features.Guests.Domain.Entities;
using PMS.Features.Reservations.Domain.Enums;
using PMS.Features.Rooms.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace PMS.Features.Reservations.Domain.Entities
{
    public class Reservation
    {
        [Key]
        public int Id { get; set; }

        //  relationships 
        public int GuestId { get; set; }
        public Guest Guest { get; set; }

        public int RoomId { get; set; }
        public Room Room { get; set; }

    
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public int NumberOfGuests { get; set; }

        // Companion
        public ICollection<Companion> Companions { get; set; } = new List<Companion>();

        //  calculation fields
        public decimal TotalAmount { get; set; } 
        public decimal DepositAmount { get; set; } 
        public decimal RemainingBalance => TotalAmount - DepositAmount; 

        public PaymentMethod PaymentMethod { get; set; }
        public BookingSource BookingSource { get; set; }
        public string? SpecialRequests { get; set; }

        [Timestamp]
        public byte[] RowVersion { get; set; }
        public ReservationStatus Status { get; set; } = ReservationStatus.Confirmed;
    }
}
