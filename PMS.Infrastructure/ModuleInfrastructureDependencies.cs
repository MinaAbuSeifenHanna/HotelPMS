using Microsoft.Extensions.DependencyInjection;
using PMS.Application.Abstractions;
using PMS.Infrastructure.Repositories;

namespace PMS.Infrastructure;

public static class ModuleInfrastructureDependencies
{
    public static IServiceCollection AddInfrastructureDependencies(this IServiceCollection services)
    {

        services.AddTransient(typeof(IRepository<>), typeof(Repository<>));
        services.AddTransient<IUnitOfWork, UnitOfWork>();
        return services;

    }
}
