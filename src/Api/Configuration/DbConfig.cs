using Microsoft.EntityFrameworkCore;
using SureProfit.Infraestructure.Data;

namespace SureProfit.Api.Configuration;

public static class DbConfig
{
    public static IServiceCollection AddDbConfig(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
        });

        return services;
    }
}
