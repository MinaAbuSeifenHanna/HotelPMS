using System;
using PMS.Features.SPA.SpaRooms.Application.DTOS;

namespace PMS.Features.SPA.SpaRooms.Application.Services;

public interface ISpaRoomService
{
        Task<IEnumerable<SpaRoomDto>> GetAllAsync();
        Task<SpaRoomDto?> GetByIdAsync(int id);
        Task<int> CreateAsync(CreateSpaRoomDto dto);
        Task<bool> UpdateAsync(int id, UpdateSpaRoomDto dto);
        Task<bool> DeleteAsync(int id);
}
