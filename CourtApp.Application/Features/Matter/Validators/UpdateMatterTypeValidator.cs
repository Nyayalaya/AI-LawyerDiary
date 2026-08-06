using CourtApp.Application.Features.Matter.Commands;
using CourtApp.Application.Features.Matter.Extention;
using FluentValidation;

public class UpdateMatterTypeValidator : AbstractValidator<UpdateMatterTypeCommand>
{
    public UpdateMatterTypeValidator()
    {
        RuleFor(x => x.Id).NotEmpty();

        this.ApplyMatterTypeRules();
    }
}