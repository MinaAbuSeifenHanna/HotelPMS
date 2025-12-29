using System;
using System.ComponentModel.DataAnnotations;
using PMS.Features.SPA.SpaBookings.Domain.Entities;
using PMS.Features.SPA.SpaRooms.Domain.Enums;

namespace PMS.Features.SPA.SpaServices.Domain.Entities
{
   public class SpaRoom
    {
        public int Id { get; set; }

        public string RoomName { get; set; } = null!;
        public SpaRoomType RoomType { get; set; }

        public bool IsAvailable { get; set; } = true;

        // Navigation
        public ICollection<SpaBooking> SpaBookings { get; set; } = new HashSet<SpaBooking>();
    }
}
