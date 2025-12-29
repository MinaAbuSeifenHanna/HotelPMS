using System;
using PMS.Core.Infrastructure.RepoIMP;
using PMS.Features.SPA.SpaBookings.Domain.Entities;
using PMS.Features.SPA.SpaBookings.Domain.IRepositry;

namespace PMS.Features.SPA.SpaBookings.Infrastructure.RepositoriesIMP;

public class SpaBookingRepository : Repository<SpaBooking>, ISpaBookingRepository
{
    public SpaBookingRepository(PMSContext context) : base(context)
    {

    }
}
