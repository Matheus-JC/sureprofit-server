using AutoMapper;
using FluentValidation;
using FluentValidation.Results;
using SureProfit.Application.Notifications;
using SureProfit.Domain.Interfaces;

namespace SureProfit.Application;

public abstract class BaseService(IUnitOfWork unitOfWork, INotifier notifier, IMapper mapper) : IUnitOfWork
{
    protected readonly IUnitOfWork _unitOfWork = unitOfWork;
    protected readonly INotifier _notifier = notifier;
    protected readonly IMapper _mapper = mapper;

    protected void Notify(ValidationResult validationResult)
    {
        foreach (var item in validationResult.Errors)
        {
            Notify(item.ErrorMessage);
        }
    }

    protected void Notify(string message)
    {
        _notifier.Handle(new Notification(message));
    }

    protected void Notify(IEnumerable<string> messages)
    {
        _notifier.Handle(messages.Select(message => new Notification(message)));
    }

    protected async Task<bool> Validate<TValidation, TClass>(TValidation validation, TClass classItem)
        where TValidation : AbstractValidator<TClass>
        where TClass : class
    {
        var validationResult = await validation.ValidateAsync(classItem);

        if (validationResult.IsValid) return true;

        Notify(validationResult);

        return false;
    }

    public async Task<bool> CommitAsync()
    {
        return await _unitOfWork.CommitAsync();
    }
}
