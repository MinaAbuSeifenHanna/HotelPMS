using PMS.DTOs;

namespace PMS.Services
{
    public interface IReservationService
    {
        Task<string> CheckInAsync(CheckInDto dto);
        Task<string> CheckOutAsync(string roomNumber);
    }
}
