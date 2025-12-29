using System;
using PMS.Features.SPA.SpaServices.Application.DTOS;

namespace PMS.Features.SPA.SpaServices.Application.Services;

public interface ISpaServiceService
{
    Task<IEnumerable<SpaServiceDto>> GetAllAsync();
    Task<SpaServiceDto?> GetByIdAsync(int id);
    Task<int> CreateAsync(CreateSpaServiceDto dto);
    Task<bool> UpdateAsync(int id, UpdateSpaServiceDto dto);
    Task<bool> DeleteAsync(int id);

}
