using System;
using System.ComponentModel.DataAnnotations;

namespace PMS.Features.SPA.SpaBookings.Application.DTOS;

public class CreateSpaBookingDto
{
    [Required]
    public int GuestId { get; set; }

    [Required]
    public int SpaServiceId { get; set; }

    [Required]
    public int TherapistId { get; set; }

    [Required]
    public int RoomId { get; set; }

    [Required]
    public DateOnly BookingDate { get; set; }

    [Required]
    public TimeOnly StartTime { get; set; }
}
