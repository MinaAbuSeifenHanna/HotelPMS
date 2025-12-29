using System;
using PMS.Features.SPA.SpaBookings.Application.DTOS;

namespace PMS.Features.SPA.SpaBookings.Application.Services;

public interface ISpaBookingService
{
    Task<IEnumerable<SpaBookingDto>> GetAllAsync(string? search = null);
    Task<SpaBookingDto?> GetByIdAsync(int id);
    Task<int> CreateAsync(CreateSpaBookingDto dto);
    Task<bool> UpdateAsync(int id, UpdateSpaBookingDto dto);
    Task<bool> CancelAsync(int id);
}
