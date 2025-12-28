using PMS.Application.Features.Reservations;

namespace PMS.Application.Services
{
    public interface IReservationService
    {

        Task<int> CreateReservationAsync(AddReservationDto dto);
        Task<bool> ProcessCheckInByIdNumberAsync(string idNumber);

        Task<bool> ProcessCheckOutByIdNumberAsync(string idNumber);
        // Task<string> CheckInAsync(CheckInDto dto);
        // Task<string> CheckOutAsync(string roomNumber);
    }
}
