using SureProfit.Application;
using SureProfit.Application.CompanyManagement;
using SureProfit.Application.CostManagement;
using SureProfit.Application.Notifications;
using SureProfit.Application.StoreManagement;
using SureProfit.Application.TagManagement;
using SureProfit.Domain;
using SureProfit.Domain.Interfaces;
using SureProfit.Domain.Services;
using SureProfit.Infra.Data.Repositories;
using SureProfit.Infra.Data.UnitOfWork;

namespace SureProfit.Api.Configuration;

public static class DependencyInjectionConfig
{
    public static IServiceCollection ResolveDependencies(this IServiceCollection services)
    {
        // Application
        services.AddScoped<INotifier, Notifier>();
        services.AddScoped<ICompanyService, CompanyService>();
        services.AddScoped<IStoreService, StoreService>();
        services.AddScoped<ITagService, TagService>();
        services.AddScoped<ICostService, CostService>();

        // Data
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<ICompanyRepository, CompanyRepository>();
        services.AddScoped<IStoreRepository, StoreRepositoy>();
        services.AddScoped<ITagRepository, TagRepository>();
        services.AddScoped<ICostRepository, CostRepository>();

        // Domain
        services.AddScoped<ICompanyUniquenessChecker, CompanyUniquenessChecker>();
        services.AddScoped<IMarkupCalculator, MarkupCalculator>();
        services.AddScoped<IVariableCostTotalRangeChecker, VariableCostTotalRangeChecker>();

        return services;
    }
}
