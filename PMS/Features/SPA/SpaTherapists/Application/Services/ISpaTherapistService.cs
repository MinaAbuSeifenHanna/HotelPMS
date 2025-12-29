using System;
using PMS.Features.SPA.SpaTherapists.Application.DTOS;

namespace PMS.Features.SPA.SpaTherapists.Application.Services;

public interface ISpaTherapistService
{
    Task<IEnumerable<SpaTherapistDto>> GetAllAsync();
    Task<SpaTherapistDto?> GetByIdAsync(int id);
    Task<int> CreateAsync(CreateSpaTherapistDto dto);
    Task<bool> UpdateAsync(int id, UpdateSpaTherapistDto dto);
    Task<bool> DeleteAsync(int id);
}
