using System.Net;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using SureProfit.Domain.Common;

namespace SureProfit.Api.Extensions;

public class ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
{
    private readonly RequestDelegate _next = next;
    private readonly ILogger<ExceptionMiddleware> _logger = logger;

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (DomainException exc)
        {
            _logger.LogError(exc, "A domain exception was thrown");
            HandleDomainException(context, exc.Message);
        }
        catch (Exception exc)
        {
            _logger.LogError(exc, "An unknown error has occurred");
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
        }
    }

    private static async void HandleDomainException(HttpContext context, string errorMessage)
    {
        var problemDetails = new ProblemDetails
        {
            Title = "An error has occurred",
            Status = (int)HttpStatusCode.BadRequest,
            Detail = errorMessage,
            Instance = context.Request.Path,
        };

        context.Response.StatusCode = (int)HttpStatusCode.BadRequest;

        context.Response.ContentType = "application/problem+json";
        await context.Response.WriteAsync(JsonSerializer.Serialize(problemDetails));
    }
}
