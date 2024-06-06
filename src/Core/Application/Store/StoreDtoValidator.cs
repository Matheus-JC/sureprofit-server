using FluentValidation;
using SureProfit.Domain.Enums;
using SureProfit.Domain.Interfaces.Data;

namespace SureProfit.Application;

public class StoreDtoValidator : AbstractValidator<StoreDto>
{
    public StoreDtoValidator(IStoreRepository storeRepository, ICompanyRepository companyRepository, bool validateId = false)
    {
        if (validateId)
        {
            RuleFor(s => s.Id)
                .NotNull().WithMessage("{PropertyName} is required")
                .NotEqual(Guid.Empty).WithMessage("{PropertyName} is invalid")
                .MustAsync((id, cancellation) => storeRepository.Exists(id)).WithMessage("{PropertyName} informed does not exist");
        }

        RuleFor(s => s.CompanyId)
            .NotNull().WithMessage("{PropertyName} is required")
            .MustAsync((id, cancellation) => companyRepository.Exists(id)).WithMessage("{PropertyName} informed does not exist")
            .NotEqual(Guid.Empty).WithMessage("{PropertyName} is invalid");

        RuleFor(s => s.Name)
            .NotEmpty().WithMessage("{PropertyName} cannot be empty")
            .Length(2, 250).WithMessage("{PropertyName} must be between {MinLength} and {MaxLength} characters");

        RuleFor(s => s.Enviroment)
            .NotNull().WithMessage("{PropertyName} is required")
            .Must(e => Enum.IsDefined(typeof(StoreEnviroment), e)).WithMessage("{PropertyName} is invalid");

        RuleFor(s => s.TargetProfit)
            .InclusiveBetween(0, 100).WithMessage("{PropertyName} is percentage and must be between {From} and {To}");
    }
}
