using PMS.Features.Reservations.Domain.Entities;
using PMS.Features.Rooms.Domain.Entities;
using PMS.Features.RoomServiceRequests.Domain.Enums;

namespace PMS.Features.RoomServiceRequests.Domain.Entities
{
    public class RoomServiceRequest
    {
        public int Id { get; set; }

        // FK
        public int RoomId { get; set; }
        public Room Room { get; set; }

        // Optional (لو الطلب تابع لحجز)
        public int? ReservationId { get; set; }
        public Reservation Reservation { get; set; }

        // Service info
        public RoomServiceType ServiceType { get; set; }
        public ServicePriority Priority { get; set; }
        public string Description { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public bool IsCompleted { get; set; } = false;
    }

}
