using System;
using System.ComponentModel.DataAnnotations;

namespace PMS.Features.SPA.SpaServices.Application.DTOS;

  public class UpdateSpaServiceDto
    {
        [Required, StringLength(100)]
        public string Name { get; set; } = null!;

        public string? Description { get; set; }

        [Required]
        public int Duration { get; set; }

        [Required]
        public decimal Price { get; set; }

        public bool IsActive { get; set; }
    }
