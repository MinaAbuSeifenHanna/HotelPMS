using System;
using System.ComponentModel.DataAnnotations;
using PMS.Features.SPA.SpaBookings.Domain.Enums;

namespace PMS.Features.SPA.SpaBookings.Application.DTOS;

public class UpdateSpaBookingDto
{
    [Required]
    public int TherapistId { get; set; }

    [Required]
    public int RoomId { get; set; }

    [Required]
    public DateOnly BookingDate { get; set; }

    [Required]
    public TimeOnly StartTime { get; set; }

    [Required]
    public SpaBookingStatus Status { get; set; }
}
