using FluentValidation;
using SureProfit.Domain.Interfaces.Data;

namespace SureProfit.Application.TagManagement;

public class TagDtoValidator : AbstractValidator<TagDto>
{
    public TagDtoValidator(ITagRepository tagRepository, bool validateId)
    {
        if (validateId)
        {
            RuleFor(t => t.Id)
                .NotNull()
                .NotEqual(Guid.Empty)
                .MustAsync((id, canceletion) => tagRepository.Exists(id)).WithMessage("{PropertyName} informed does not exist");
        }

        RuleFor(t => t.Name)
            .NotEmpty()
            .Length(2, 50);

        RuleFor(t => t.Active)
            .NotNull();
    }
}
