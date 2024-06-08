using System.Data;
using FluentValidation;
using SureProfit.Domain;
using SureProfit.Domain.Interfaces.Data;

namespace SureProfit.Application;

public class CostDtoValidator : AbstractValidator<CostDto>
{
    public CostDtoValidator(ICostRepository costRepository, IStoreRepository storeRepository,
        ITagRepository tagRepository, bool validateId)
    {
        if (validateId)
        {
            RuleFor(c => c.Id)
                .NotNull()
                .NotEqual(Guid.Empty)
                .MustAsync((id, cancelation) => costRepository.Exists(id)).WithMessage("{PropertyName} informed does not exist");
        }

        RuleFor(c => c.StoreId)
            .NotNull()
            .NotEqual(Guid.Empty)
            .MustAsync((id, cancelation) => storeRepository.Exists(id)).WithMessage("{PropertyName} informed does not exist");

        RuleFor(c => c.TagId)
            .NotEqual(Guid.Empty)
            .MustAsync((id, cancelation) => id is not null ? tagRepository.Exists(id.Value) : Task.FromResult(true))
                .WithMessage("{PropertyName} informed does not exist");

        RuleFor(c => c.Description)
            .NotEmpty()
            .Length(2, 50);

        RuleFor(c => c.Type)
            .NotNull()
            .IsInEnum();

        RuleFor(c => c.Value)
            .NotNull()
            .GreaterThan(0).When(c => c.Type == CostType.Fixed, ApplyConditionTo.CurrentValidator)
                .WithMessage($"{nameof(CostType.Fixed)} cost must have a value greater than 0")
            .InclusiveBetween(0, 100).When(c => c.Type == CostType.Percentage, ApplyConditionTo.CurrentValidator)
                .WithMessage($"{nameof(CostType.Percentage)} type costs must have a value between 0 and 100");
    }
}
