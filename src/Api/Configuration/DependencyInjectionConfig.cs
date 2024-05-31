using SureProfit.Application;
using SureProfit.Application.Notifications;
using SureProfit.Domain;
using SureProfit.Domain.Interfaces.Data;
using SureProfit.Infra.Data;

namespace SureProfit.Api.Configuration;

public static class DependencyInjectionConfig
{
    public static IServiceCollection ResolveDependencies(this IServiceCollection services)
    {
        // Application
        services.AddScoped<ICompanyService, CompanyService>();
        services.AddScoped<INotifier, Notifier>();

        // Data
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<ICompanyRepository, CompanyRepository>();

        return services;
    }
}
