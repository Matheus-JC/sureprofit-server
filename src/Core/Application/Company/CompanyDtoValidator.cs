using FluentValidation;
using SureProfit.Domain.ValueObjects;

namespace SureProfit.Application;

public class CompanyDtoValidator : AbstractValidator<CompanyDto>
{
    public CompanyDtoValidator()
    {
        RuleFor(c => c.Name)
            .NotEmpty().WithMessage("{PropertyName} cannot be empty")
            .Length(2, 250).WithMessage("{PropertyName} must be between {MinLength} and {MaxLength} characters");

        RuleFor(c => c.Cnpj == null || Cnpj.IsValid(c.Cnpj))
            .Equal(true)
            .WithMessage("CNPJ is invalid");
    }
}
