using System;
using PMS.Features.SPA.SpaBookings.Domain.Enums;

namespace PMS.Features.SPA.SpaBookings.Application.DTOS;

public class SpaBookingDto
{
    public int Id { get; set; }

    public int GuestId { get; set; }
    public string GuestName { get; set; } = null!;

    public int SpaServiceId { get; set; }
    public string SpaServiceName { get; set; } = null!;

    public int TherapistId { get; set; }
    public string TherapistName { get; set; } = null!;

    public int RoomId { get; set; }
    public string RoomName { get; set; } = null!;

    public DateOnly BookingDate { get; set; }
    public TimeOnly StartTime { get; set; }

    public SpaBookingStatus Status { get; set; }
}
