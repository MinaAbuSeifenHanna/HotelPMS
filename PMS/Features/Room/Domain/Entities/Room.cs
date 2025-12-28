using PMS.Features.Rooms.Domain.Enums;
using PMS.Features.Reservations.Domain.Entities;

using System.ComponentModel.DataAnnotations;
using PMS.Features.RoomServiceRequests.Domain.Enums;
using PMS.Features.RoomServiceRequests.Domain.Entities;

namespace PMS.Features.Rooms.Domain.Entities
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
        public ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();

        // Housekeeping relationship
        [Required]
        public HousekeepingStatus HousekeepingStatus { get; set; } = HousekeepingStatus.Cleaned;


        // 
        public ICollection<RoomServiceRequest> ServiceRequests { get; set; } = new List<RoomServiceRequest>();

    }

}
