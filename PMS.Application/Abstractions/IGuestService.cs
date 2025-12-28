using PMS.Application.Common;
using PMS.Application.Features.Guests.DTOs;
namespace PMS.Application.Services
{
    public interface IGuestService
    {
        Task<Result<int>> AddGuestAsync(AddGuestDto dto);
        Task<Result<IReadOnlyList<GuestResultDto>>> GetAllGuestsAsync(string? search = null, int pageNumber = 1, int pageSize = 10);

        Task<Result<GuestResultDto>> GetGuestByNumberIdAsync( string idNumber);
        Task<Result<GuestResultDto>> GetGuestByIdAsync(int guestId);
        Task<Result<string>> UpdateGuestAsync(string idNumber, UpdateGuestDto dto);
         Task<Result<string>> DeleteGuestAsync(string idNumber);

        // vip guest related operations can be added here in the future
        // Task<IEnumerable<GuestResultDto>> GetVipGuestsAsync();


    }
}
