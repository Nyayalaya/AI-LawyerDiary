using CourtApp.Application.Features.Matter.Commands;
using FluentValidation;

namespace CourtApp.Application.Features.Matter.Validators;

public class DeleteMatterTypeValidator
    : AbstractValidator<DeleteMatterTypeCommand>
{
    public DeleteMatterTypeValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty();
    }
}