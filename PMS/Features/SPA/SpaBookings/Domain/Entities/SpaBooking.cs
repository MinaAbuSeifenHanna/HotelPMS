using System;
using System.ComponentModel.DataAnnotations;
using PMS.Features.Guests.Domain.Entities;
using PMS.Features.SPA.SpaBookings.Domain.Enums;
using PMS.Features.SPA.SpaServices.Domain.Entities;
using PMS.Features.SPA.SpaTherapists.Domain.Entities;

namespace PMS.Features.SPA.SpaBookings.Domain.Entities
{
    public class SpaBooking
    {
        public int Id { get; set; }

        // FK → Guest
        public int GuestId { get; set; }
        public Guest Guest { get; set; } = null!;

        // FK → SpaService
        public int SpaServiceId { get; set; }
        public SpaService SpaService { get; set; } = null!;

        // FK → Therapist
        public int TherapistId { get; set; }
        public SpaTherapist Therapist { get; set; } = null!;

        // FK → Room
        public int RoomId { get; set; }
        public SpaRoom Room { get; set; } = null!;

        public DateOnly BookingDate { get; set; }
        public TimeOnly StartTime { get; set; }

        public SpaBookingStatus Status { get; set; } = SpaBookingStatus.Pending;
    }
}
