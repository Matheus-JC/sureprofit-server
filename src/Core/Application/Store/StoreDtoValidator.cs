using FluentValidation;
using SureProfit.Domain.Interfaces.Data;

namespace SureProfit.Application;

public class StoreDtoValidator : AbstractValidator<StoreDto>
{
    public StoreDtoValidator(IStoreRepository storeRepository, ICompanyRepository companyRepository, bool validateId = false)
    {
        if (validateId)
        {
            RuleFor(s => s.Id)
                .NotNull()
                .NotEqual(Guid.Empty)
                .MustAsync((id, cancellation) => storeRepository.Exists(id)).WithMessage("{PropertyName} informed does not exist");
        }

        RuleFor(s => s.CompanyId)
            .NotNull()
            .MustAsync((id, cancellation) => companyRepository.Exists(id)).WithMessage("{PropertyName} informed does not exist")
            .NotEqual(Guid.Empty);

        RuleFor(s => s.Name)
            .NotEmpty()
            .Length(2, 250);

        RuleFor(s => s.Enviroment)
            .NotNull()
            .IsInEnum();

        RuleFor(s => s.TargetProfit)
            .InclusiveBetween(0, 100);
    }
}
