using PMS.Domain.enums;
using System.ComponentModel.DataAnnotations;

namespace PMS.Domain.entity
{
    public class Reservation
    {
        [Key]
        public int Id { get; set; }
        //-------------------- Room Connection --------------------
        // (Foreign Key)
        [Required]
        public int RoomId { get; set; }
        // Navigation Property
        public Room Room { get; set; }

        //-------------------- Guest Connection --------------------

        // (Foreign Key)
        [Required]
        public int GuestId { get; set; }

        // Navigation Property
        public Guest Guest { get; set; }
        //-------------------- Reservation Details --------------------
        public DateTime CheckInDate { get; set; }

        // Nullable to allow for future check-outs
        public DateTime? CheckOutDate { get; set; } 

        public decimal TotalAmount { get; set; }
        public bool IsCheckedOut { get; set; } = false;


        public ReservationStatus Status { get; set; } = ReservationStatus.Created;
       








    }
}
