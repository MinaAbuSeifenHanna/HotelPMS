using PMS.Features.Rooms.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace PMS.Features.Rooms.Application.DTOS
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
