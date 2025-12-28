using PMS.Features.Guests.Application.DTOS;

namespace PMS.Features.Guests.Application.Services
{
    public interface IGuestService
    {
        Task<int> AddGuestAsync(AddGuestDto dto);
        Task<IEnumerable<GuestResultDto>> GetAllGuestsAsync();

        Task<GuestResultDto> GetGuestByIdAsync(string idNumber);
        Task<bool> UpdateGuestAsync(string idNumber, UpdateGuestDto dto);

        Task<bool> DeleteGuestByIdNumberAsync(string idNumber);
    }
}
