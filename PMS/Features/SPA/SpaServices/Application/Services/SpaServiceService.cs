using System;
using AutoMapper;
using PMS.Features.SPA.SpaServices.Application.DTOS;
using PMS.Features.SPA.SpaServices.Domain.Entities;
using PMS.Features.SPA.SpaServices.Domain.IRepositry;

namespace PMS.Features.SPA.SpaServices.Application.Services;

public class SpaServiceService : ISpaServiceService
{
    private readonly ISpaServiceRepository _spaServiceRepositor;
    private readonly IMapper _mapper;
    public SpaServiceService(ISpaServiceRepository spaServiceRepositor, IMapper mapper)
    {
        _spaServiceRepositor = spaServiceRepositor;
        _mapper = mapper;
    }
    public async Task<int> CreateAsync(CreateSpaServiceDto dto)
    {
        var spaService = _mapper.Map<SpaService>(dto);
        await _spaServiceRepositor.AddAsync(spaService);
        await _spaServiceRepositor.SaveChangesAsync();
        return spaService.Id;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var spaService = await _spaServiceRepositor.GetByIdAsync(id);
        if (spaService == null) return false;
        spaService.IsActive = false;


        _spaServiceRepositor.Update(spaService);
        await _spaServiceRepositor.SaveChangesAsync();


        return true;
    }

    public async Task<IEnumerable<SpaServiceDto>> GetAllAsync()
    {
        var services = await _spaServiceRepositor.GetAllAsync();
        return _mapper.Map<IEnumerable<SpaServiceDto>>(services);
    }

    public async Task<SpaServiceDto?> GetByIdAsync(int id)
    {
        var service = await _spaServiceRepositor.GetByIdAsync(id);
        return service == null ? null : _mapper.Map<SpaServiceDto>(service);
    }

    public async Task<bool> UpdateAsync(int id, UpdateSpaServiceDto dto)
    {
        var spaService = await _spaServiceRepositor.GetByIdAsync(id);
        if (spaService == null) return false;

        _mapper.Map(dto, spaService);

        _spaServiceRepositor.Update(spaService);
        await _spaServiceRepositor.SaveChangesAsync();

        return true;
    }
}
