using System;
using PMS.Core.Infrastructure.RepoIMP;
using PMS.Features.SPA.SpaRooms.Domain.IRepositry;
using PMS.Features.SPA.SpaServices.Domain.Entities;
using PMS.Features.SPA.SpaServices.Domain.IRepositry;

namespace PMS.Features.SPA.SpaRooms.Infrastructure.RepositoriesIMP;

public class SpaRoomRepository : Repository<SpaRoom>, ISpaRoomRepositry
{
    public SpaRoomRepository(PMSContext context) : base(context)
    {

    }

}
