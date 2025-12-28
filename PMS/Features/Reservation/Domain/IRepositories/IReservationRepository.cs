using PMS.Core.Domain.Interfaces;
using PMS.Features.Reservations.Domain.Entities;

namespace PMS.Features.Reservations.Domain.IRepositories
{
    public interface IReservationRepository : IRepository<Reservation>
    {
        Task<bool> HasConflictAsync(int roomId, DateTime checkIn, DateTime checkOut);

        Task<Reservation?> GetActiveReservationByIdNumberAsync(string idNumber);
        Task<IEnumerable<Reservation>> GetAllWithDetailsAsync();
    }
}
