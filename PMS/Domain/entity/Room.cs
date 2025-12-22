using PMS.Domain.enums;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace PMS.Domain.entity
{
    public class Room
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string RoomNumber { get; set; } 

        public string RoomType { get; set; } // Single, Double, Suite

        public decimal PricePerNight { get; set; }

        public RoomStatus Status { get; set; } = RoomStatus.Available;

        // one-to-Many relationship with Reservation
        // one room can have many reservations

        [JsonIgnore]
        public ICollection<Reservation>? Reservations { get; set; }
       
    }
}
