using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PMS.Features.SPA.SpaBookings.Domain.Entities;

namespace PMS.Features.SPA.SpaServices.Domain.Entities
{
  public class SpaService
  {
    public int Id { get; set; }
    [Required, StringLength(100)]
    public required string Name { get; set; } = null!;
    public string? Description { get; set; }
    public required int Duration { get; set; }
    [Column(TypeName = "decimal(18,2)")]
    public required decimal Price { get; set; }
    public bool IsActive { get; set; } = true;
   public ICollection<SpaBooking>? SpaBookings { get; set; } = new HashSet<SpaBooking>();
  }
}
