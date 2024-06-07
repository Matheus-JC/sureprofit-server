using FluentValidation;
using SureProfit.Domain.Interfaces.Data;

namespace SureProfit.Application;

public class TagDtoValidator : AbstractValidator<TagDto>
{
    public TagDtoValidator(ITagRepository tagRepository, bool validateId)
    {
        if (validateId)
        {
            RuleFor(t => t.Id)
                .NotNull().WithMessage("{PropertyName} is required")
                .NotEqual(Guid.Empty).WithMessage("{PropertyName} cannot be empty")
                .MustAsync((id, canceletion) => tagRepository.Exists(id)).WithMessage("{PropertyName} informed does not exist");
        }

        RuleFor(t => t.Name)
            .NotEmpty().WithMessage("{PropertyName} cannot be empty")
            .Length(2, 50).WithMessage("{PropertyName} must be between {MinLength} and {MaxLength} characters");

        RuleFor(t => t.Active)
            .NotNull().WithMessage("{PropertyName} is required");
    }
}
