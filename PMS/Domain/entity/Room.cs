using PMS.Domain.enums.RoomEnums;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace PMS.Domain.entity
{
    public class Room
    {
        [Key]
        public int Id { get; set; }

        [Required, StringLength(10)]
        public string RoomNumber { get; set; } 

        [Required]
        public RoomType Type { get; set; } // Enum

        [Required]
        public int Floor { get; set; } 

        [Required]
        public decimal PricePerNight { get; set; }

        public RoomStatus Status { get; set; } = RoomStatus.Available; // Enum

        // for optimistic concurrency control
        [Timestamp]
        public byte[] RowVersion { get; set; }

        // Relationships
        public ICollection<Reservation> Reservations { get; set; }
    }
}
