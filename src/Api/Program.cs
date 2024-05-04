using Api.Configuration;
using Api.Configuration.Swagger;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddApiVersioningConfig();

builder.Services.AddSwaggerConfig();

builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwaggerConfig(app.DescribeApiVersions());
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
