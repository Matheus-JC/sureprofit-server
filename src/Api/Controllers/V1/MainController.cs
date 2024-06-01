using System.Net;
using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using SureProfit.Application.Notifications;

namespace SureProfit.Api.Controllers.V1;

[ApiController]
[ApiVersion("1.0")]
public class MainController(INotifier notifier) : ControllerBase
{
    private readonly INotifier _notifier = notifier;

    protected bool IsOperationInvalid()
    {
        return _notifier.HasNotification();
    }

    protected BadRequestObjectResult HandleBadRequest(ModelStateDictionary modelState)
    {
        if (!modelState.IsValid)
        {
            NotifyErrorModelStateInvalid(modelState);
        }

        return HandleBadRequest();
    }

    protected void NotifyErrorModelStateInvalid(ModelStateDictionary modelState)
    {
        var errors = modelState.Values.SelectMany(e => e.Errors);

        foreach (var error in errors)
        {
            var errorMessage = error.Exception is null ? error.ErrorMessage : error.Exception.Message;
            Notify(errorMessage);
        }
    }

    protected void Notify(string errorMessage)
    {
        _notifier.Handle(new Notification(errorMessage));
    }

    protected BadRequestObjectResult HandleBadRequest()
    {
        return BadRequest(GenerateBadRequestProblemDetails());
    }

    private ProblemDetails GenerateBadRequestProblemDetails()
    {
        var problemDetails = new ProblemDetails
        {
            Status = (int)HttpStatusCode.BadRequest,
            Title = "One or more validation errors occurred.",
            Detail = "See the errors property for details.",
            Instance = HttpContext.Request.Path,
        };

        problemDetails.Extensions.Add("errors",
            _notifier.GetNotifications().Select(n => n.Message));

        return problemDetails;
    }
}
