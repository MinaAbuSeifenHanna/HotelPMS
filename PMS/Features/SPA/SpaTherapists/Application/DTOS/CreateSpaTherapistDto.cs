using System;
using System.ComponentModel.DataAnnotations;

namespace PMS.Features.SPA.SpaTherapists.Application.DTOS;

public class CreateSpaTherapistDto
{
    [Required, StringLength(100)]
    public string FullName { get; set; } = null!;

    [Required, StringLength(100)]
    public string Specialization { get; set; } = null!;

    [Required, Phone]
    public string Phone { get; set; } = null!;
}
