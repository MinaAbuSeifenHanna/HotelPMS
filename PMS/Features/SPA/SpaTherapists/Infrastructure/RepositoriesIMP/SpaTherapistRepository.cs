using System;
using PMS.Core.Infrastructure.RepoIMP;
using PMS.Features.SPA.SpaTherapists.Domain.Entities;
using PMS.Features.SPA.SpaTherapists.Domain.IRepositry;

namespace PMS.Features.SPA.SpaTherapists.Infrastructure.RepositoriesIMP;

public class SpaTherapistRepository:Repository<SpaTherapist>,ISpaTherapistRepository
{
    public SpaTherapistRepository(PMSContext context) : base(context)
    {
        
    }

}
