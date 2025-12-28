using Microsoft.EntityFrameworkCore;
using PMS.Core.Infrastructure.RepoIMP;
using PMS.Features.Guests.Domain.Entities;
using PMS.Features.Guests.Domain.IRepositories;

namespace PMS.Features.Guests.Infrastructure.RepositoriesIMP
{
    // PMS.Core.Infrastructure.RepoIMP
    public class GuestRepository : Repository<Guest>, IGuestRepository
    {
        public GuestRepository(PMSContext context) : base(context) { }

        public async Task<Guest?> GetByIdNumberAsync(string idNumber)
        {
            return await _dbSet
                .Include(g => g.Reservations)
                .FirstOrDefaultAsync(g => g.IdNumber == idNumber);
        }

        public async Task<bool> IsEmailOrIdUniqueAsync(string email, string idNumber)
        {
            return !await _dbSet.AnyAsync(g => g.Email == email || g.IdNumber == idNumber);
        }
    
    }
}
