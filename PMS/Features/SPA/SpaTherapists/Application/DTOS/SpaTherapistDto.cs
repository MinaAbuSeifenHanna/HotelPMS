using System;

namespace PMS.Features.SPA.SpaTherapists.Application.DTOS;

public class SpaTherapistDto
{
    public int Id { get; set; }
    public string FullName { get; set; } = null!;
    public string Specialization { get; set; } = null!; 
    public string Phone { get; set; } = null!;
    public bool IsAvailable { get; set; }
}
