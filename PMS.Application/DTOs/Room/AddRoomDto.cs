using PMS.Domain.enums.RoomEnums;
using System.ComponentModel.DataAnnotations;

namespace PMS.DTOs.Room
{
    public class AddRoomDto
    {
        [Required]
        public string RoomNumber { get; set; }
        public RoomType Type { get; set; }
        public int Floor { get; set; }
        public decimal PricePerNight { get; set; }
        public RoomStatus Status { get; set; } // Default is Available
    }
}
