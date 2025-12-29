using System;
using AutoMapper;
using PMS.Features.SPA.SpaRooms.Application.DTOS;
using PMS.Features.SPA.SpaRooms.Domain.IRepositry;
using PMS.Features.SPA.SpaServices.Application.DTOS;
using PMS.Features.SPA.SpaServices.Application.Services;
using PMS.Features.SPA.SpaServices.Domain.Entities;

namespace PMS.Features.SPA.SpaRooms.Application.Services;

public class SpaRoomService : ISpaRoomService
{
    private readonly IMapper _mapper;
    private readonly ISpaRoomRepositry __spaRoomRepository;

    public SpaRoomService(IMapper mapper, ISpaRoomRepositry spaRoomRepositry)
    {
        _mapper = mapper;
        __spaRoomRepository = spaRoomRepositry;
    }

    public async Task<int> CreateAsync(CreateSpaRoomDto dto)
    {
        var room = _mapper.Map<SpaRoom>(dto);

        await __spaRoomRepository.AddAsync(room);
        await __spaRoomRepository.SaveChangesAsync();

        return room.Id;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var room = await __spaRoomRepository.GetByIdAsync(id);
        if (room == null) return false;
         room.IsAvailable = false;
        __spaRoomRepository.Update(room);
        await __spaRoomRepository.SaveChangesAsync();

        return true;
    }

    public async Task<IEnumerable<SpaRoomDto>> GetAllAsync()
    {
        var rooms = await __spaRoomRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<SpaRoomDto>>(rooms);
    }

    public async Task<SpaRoomDto?> GetByIdAsync(int id)
    {
        var room = await __spaRoomRepository.GetByIdAsync(id);
        return room == null ? null : _mapper.Map<SpaRoomDto>(room);
    }

    public async Task<bool> UpdateAsync(int id, UpdateSpaRoomDto dto)
    {
        var room = await __spaRoomRepository.GetByIdAsync(id);
        if (room == null) return false;

        _mapper.Map(dto, room);

        __spaRoomRepository.Update(room);
        await __spaRoomRepository.SaveChangesAsync();

        return true;
    }
}
