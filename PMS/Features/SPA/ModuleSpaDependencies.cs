
using PMS.Features.SPA.SpaBookings.Application.Services;
using PMS.Features.SPA.SpaBookings.Domain.IRepositry;
using PMS.Features.SPA.SpaBookings.Infrastructure.RepositoriesIMP;
using PMS.Features.SPA.SpaRooms.Application.Services;
using PMS.Features.SPA.SpaRooms.Domain.IRepositry;
using PMS.Features.SPA.SpaRooms.Infrastructure.RepositoriesIMP;
using PMS.Features.SPA.SpaServices.Application.Services;
using PMS.Features.SPA.SpaServices.Domain.IRepositry;
using PMS.Features.SPA.SpaServices.Infrastructure.RepositoriesIMP;
using PMS.Features.SPA.SpaTherapists.Application.Services;
using PMS.Features.SPA.SpaTherapists.Domain.IRepositry;
using PMS.Features.SPA.SpaTherapists.Infrastructure.RepositoriesIMP;

namespace PMS.Features.SPA;

public static class ModuleSpaDependencies
{
    public static IServiceCollection AddSpaDependencies(this IServiceCollection services)
    {

        services.AddTransient<ISpaBookingService, SpaBookingService>();
        services.AddTransient<ISpaBookingRepository, SpaBookingRepository>();

        services.AddTransient<ISpaTherapistRepository, SpaTherapistRepository>();
        services.AddTransient<ISpaTherapistService, SpaTherapistService>();

        services.AddTransient<ISpaServiceRepository, SpaServiceRepository>();
        services.AddTransient<ISpaServiceService, SpaServiceService>();

        services.AddTransient<ISpaRoomService, SpaRoomService>();
        services.AddTransient<ISpaRoomRepositry, SpaRoomRepository>();

        return services;

    }
}
