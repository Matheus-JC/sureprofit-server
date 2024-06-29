using Asp.Versioning.ApiExplorer;
using Microsoft.OpenApi.Models;

namespace SureProfit.Api.Configuration.Swagger;

public static class SwaggerCongiration
{
    public static IServiceCollection AddSwaggerConfig(this IServiceCollection services)
    {
        services.AddSwaggerGen(options =>
        {
            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Description = "Insert the JWT token like this: Bearer {your token}",
                Name = "Authorization",
                Scheme = "Bearer",
                BearerFormat = "JWT",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.ApiKey
            });

            options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    Array.Empty<string>()
                }
            });
        });

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
