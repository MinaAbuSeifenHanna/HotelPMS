using System.Reflection;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using PMS.Application.Services;

namespace PMS.Application;

public static class ModuleApplicationDependencies
{
    public static IServiceCollection AddApplicationDependencies(this IServiceCollection services)
    {
        // Add AutoMapper profiles
        services.AddAutoMapper(Assembly.GetExecutingAssembly());

        // Add FluentValidation validators
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        services.AddTransient<IGuestService,GuestService>();

        return services;

    }

}
