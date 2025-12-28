using PMS.Core.Domain.Interfaces;
using PMS.Features.Guests.Domain.Entities;

namespace PMS.Features.Guests.Domain.IRepositories
{
    public interface IGuestRepository : IRepository<Guest>
    {
        Task<Guest?> GetByIdNumberAsync(string idNumber);
        Task<bool> IsEmailOrIdUniqueAsync(string email, string idNumber);
    }
}
