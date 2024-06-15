using FluentValidation;
using SureProfit.Domain.Interfaces;
using SureProfit.Domain.ValueObjects;

namespace SureProfit.Application.CompanyManagement;

public class CompanyDtoValidator : AbstractValidator<CompanyDto>
{
    public CompanyDtoValidator(ICompanyRepository companyRepository, bool validateId = false)
    {
        if (validateId)
        {
            RuleFor(s => s.Id)
                .NotNull()
                .NotEqual(Guid.Empty)
                .MustAsync((id, cancellation) => companyRepository.Exists(id)).WithMessage("{PropertyName} informed does not exist");
        }

        RuleFor(c => c.Name)
            .NotEmpty()
            .Length(2, 250);

        RuleFor(c => c.Cnpj == null || Cnpj.IsValid(c.Cnpj))
            .Equal(true).WithMessage("CNPJ is invalid");
    }
}
