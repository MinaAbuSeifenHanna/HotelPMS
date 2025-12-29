using System;

namespace PMS.Features.SPA.SpaServices.Application.DTOS;

 public class SpaServiceDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public int Duration { get; set; }
        public decimal Price { get; set; }
        public bool IsActive { get; set; }
    }
