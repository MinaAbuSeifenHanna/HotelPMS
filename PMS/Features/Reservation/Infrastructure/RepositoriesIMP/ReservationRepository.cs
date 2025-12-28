using Microsoft.EntityFrameworkCore;
using PMS.Core.Infrastructure.RepoIMP;
using PMS.Features.Reservations.Domain.Entities;
using PMS.Features.Reservations.Domain.Enums;
using PMS.Features.Reservations.Domain.IRepositories;

namespace PMS.Features.Reservations.Infrastructure.RepositoriesIMP
{
    public class ReservationRepository : Repository<Reservation>, IReservationRepository
    {
        public ReservationRepository(PMSContext context) : base(context) { }

        public async Task<bool> HasConflictAsync(int roomId, DateTime checkIn, DateTime checkOut)
        {
            return await _dbSet.AnyAsync(r =>
                r.RoomId == roomId &&
                r.Status != ReservationStatus.Cancelled &&
                r.Status != ReservationStatus.CheckedOut &&
                ((checkIn < r.CheckOutDate) && (checkOut > r.CheckInDate)));
        }

        public async Task<Reservation?> GetActiveReservationByIdNumberAsync(string idNumber)
        {
            return await _dbSet
                .Include(r => r.Guest)
                .Include(r => r.Room)
                .Include(r => r.Companions)
                .FirstOrDefaultAsync(r => r.Guest.IdNumber == idNumber &&
                                        (r.Status == ReservationStatus.Confirmed || r.Status == ReservationStatus.CheckedIn));
        }

        public async Task<IEnumerable<Reservation>> GetAllWithDetailsAsync()
        {
            return await _dbSet
                .Include(r => r.Guest)
                .Include(r => r.Room)
                .Include(r => r.Companions)
                .OrderByDescending(r => r.CheckInDate)
                .ToListAsync();
        }
    }
}
