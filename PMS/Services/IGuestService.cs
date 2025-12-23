using PMS.DTOs.Guest;

namespace PMS.Services
{
    public interface IGuestService
    {
        Task<int> AddGuestAsync(AddGuestDto dto);
        Task<IEnumerable<GuestResultDto>> GetAllGuestsAsync(); 

        Task<GuestResultDto> GetGuestByIdAsync(string idNumber); 
        Task<bool> UpdateGuestAsync(string idNumber, UpdateGuestDto dto);

        Task<bool> DeleteGuestByIdNumberAsync(string idNumber);

        // vip guest related operations can be added here in the future
        // Task<IEnumerable<GuestResultDto>> GetVipGuestsAsync();


    }
}
