using System;
using System.ComponentModel.DataAnnotations;
using PMS.Features.SPA.SpaBookings.Domain.Entities;

namespace PMS.Features.SPA.SpaTherapists.Domain.Entities
{
  public class SpaTherapist
  {
    public int Id { get; set; }

    public string FullName { get; set; } = null!;
    public string Specialization { get; set; } = null!;
    public string Phone { get; set; } = null!;

    public bool IsAvailable { get; set; } = true;

    // Navigation
    public ICollection<SpaBooking> SpaBookings { get; set; } = new HashSet<SpaBooking>();
  }
}
