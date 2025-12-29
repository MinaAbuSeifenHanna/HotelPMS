using System;
using PMS.Core.Infrastructure.RepoIMP;
using PMS.Features.SPA.SpaServices.Domain.Entities;
using PMS.Features.SPA.SpaServices.Domain.IRepositry;

namespace PMS.Features.SPA.SpaServices.Infrastructure.RepositoriesIMP;

public class SpaServiceRepository : Repository<SpaService>, ISpaServiceRepository
{
    public SpaServiceRepository(PMSContext context) : base(context)
    {

    }

}
