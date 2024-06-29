using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SureProfit.Infraestructure.Identity;

namespace SureProfit.Api.Configuration;

public static class IdentityConfig
{
    public static IServiceCollection AddIdentityConfig(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ApiIdentityDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

        services.AddIdentity<ApplicationUser, IdentityRole>()
            .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<ApiIdentityDbContext>();

        return services;
    }
}
