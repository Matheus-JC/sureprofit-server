using SureProfit.Api.Extensions;
using SureProfit.Application.AuthorizationManagement;
using SureProfit.Application.CompanyManagement;
using SureProfit.Application.CostManagement;
using SureProfit.Application.Notifications;
using SureProfit.Application.StoreManagement;
using SureProfit.Application.TagManagement;
using SureProfit.Domain.Interfaces;
using SureProfit.Domain.Services;
using SureProfit.Infraestructure.Data.Repositories;
using SureProfit.Infraestructure.Data.UnitOfWork;
using SureProfit.Infraestructure.Identity;
using SureProfit.Infraestructure.JwtToken;

namespace SureProfit.Api.Configuration;

public static class DependencyInjectionConfig
{
    public static IServiceCollection ResolveDependencies(this IServiceCollection services)
    {
        // API
        services.AddScoped<IUserContext, UserContext>();

        // Application
        services.AddScoped<INotifier, Notifier>();
        services.AddScoped<ICompanyService, CompanyService>();
        services.AddScoped<IStoreService, StoreService>();
        services.AddScoped<ITagService, TagService>();
        services.AddScoped<ICostService, CostService>();
        services.AddScoped<IAuthService, AuthService>();

        // Infraestructure
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<ICompanyRepository, CompanyRepository>();
        services.AddScoped<IStoreRepository, StoreRepositoy>();
        services.AddScoped<ITagRepository, TagRepository>();
        services.AddScoped<ICostRepository, CostRepository>();
        services.AddScoped<IIdentityService, IdentityService>();
        services.AddScoped<IAuthTokenGenerator, JwtTokenGenerator>();

        // Domain
        services.AddScoped<ICompanyUniquenessChecker, CompanyUniquenessChecker>();
        services.AddScoped<IMarkupCalculator, MarkupCalculator>();
        services.AddScoped<IVariableCostTotalRangeChecker, VariableCostTotalRangeChecker>();

        return services;
    }
}
