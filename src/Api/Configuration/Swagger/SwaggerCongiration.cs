using Asp.Versioning.ApiExplorer;

namespace SureProfit.Api.Configuration.Swagger;

public static class SwaggerCongiration
{
    public static IServiceCollection AddSwaggerConfig(this IServiceCollection services)
    {
        services.AddSwaggerGen();
        services.ConfigureOptions<ConfigureSwaggerOptions>();

        return services;
    }

    public static IApplicationBuilder UseSwaggerConfig(this IApplicationBuilder app, IReadOnlyList<ApiVersionDescription> descriptions)
    {
        app.UseSwagger();
        app.UseSwaggerUI(options =>
        {
            foreach (var description in descriptions)
            {
                options.SwaggerEndpoint(
                    $"/swagger/{description.GroupName}/swagger.json",
                    description.GroupName.ToUpperInvariant()
                );
            }
        });

        return app;
    }
}
