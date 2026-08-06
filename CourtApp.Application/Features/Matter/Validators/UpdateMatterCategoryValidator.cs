using CourtApp.Application.Features.Matter.Commands;
using CourtApp.Application.Features.Matter.Commands.MatterCategory.Update;
using CourtApp.Application.Features.Matter.Extention;
using FluentValidation;

namespace CourtApp.Application.Features.Matter.Validators;

public class UpdateMatterCategoryValidator
    : AbstractValidator<UpdateMatterCategoryCommand>
{
    public UpdateMatterCategoryValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty();

        this.ApplyMatterCategoryRules();
    }
}