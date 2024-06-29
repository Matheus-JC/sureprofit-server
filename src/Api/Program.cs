using SureProfit.Api.Configuration;
using SureProfit.Api.Configuration.Swagger;
using SureProfit.Api.Extensions;
using SureProfit.Application.Mappings;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpContextAccessor();

builder.Services.AddDbConfig(builder.Configuration);
builder.Services.AddIdentityConfig(builder.Configuration);
builder.Services.AddAuthConfig(builder.Configuration);
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

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.UseMiddleware<ExceptionMiddleware>();

app.Run();
