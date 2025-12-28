using PMS.Features.Reservations.Application.DTOS;

namespace PMS.Features.Reservations.Application.Services
{
    public interface IReservationService
    {

        Task<int> CreateReservationAsync(AddReservationDto dto);
        Task<bool> ProcessCheckInByIdNumberAsync(string idNumber);

        Task<bool> ProcessCheckOutByIdNumberAsync(string idNumber);

        Task<bool> CancelReservationByGuestIdAsync(string idNumber);
        Task<IEnumerable<ReservationResultDto>> GetAllReservationsAsync();
        // Task<string> CheckInAsync(CheckInDto dto);
        // Task<string> CheckOutAsync(string roomNumber);
    }
}
