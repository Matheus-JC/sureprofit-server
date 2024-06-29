using FluentValidation;

namespace SureProfit.Application.AuthorizationManagement;

public class RegisterUserDtoValidator : AbstractValidator<RegisterUserDto>
{
    public RegisterUserDtoValidator()
    {
        RuleFor(u => u.Email)
            .NotEmpty()
            .EmailAddress();

        RuleFor(u => u.Password)
            .NotEmpty();

        RuleFor(u => u.ConfirmPassword)
            .Equal(u => u.Password).WithMessage("Password confirmation does not match");
    }
}
