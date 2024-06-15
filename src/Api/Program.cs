using Microsoft.EntityFrameworkCore;
using SureProfit.Api.Configuration;
using SureProfit.Api.Configuration.Swagger;
using SureProfit.Api.Extensions;
using SureProfit.Application.Mappings;
using SureProfit.Infra.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnections"));
});

builder.Services.AddAutoMapper(typeof(DomainToDtoMappingProfile), typeof(DtoToDomainMappingProfile));

builder.Services.AddControllers()
    .ConfigureApiBehaviorOptions(opt =>
    {
        opt.SuppressModelStateInvalidFilter = true;
    });

builder.Services.AddApiVersioningConfig();

builder.Services.AddSwaggerConfig();

builder.Services.AddEndpointsApiExplorer();

builder.Services.ResolveDependencies();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwaggerConfig(app.DescribeApiVersions());
}

// app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseMiddleware<ExceptionMiddleware>();

app.Run();
