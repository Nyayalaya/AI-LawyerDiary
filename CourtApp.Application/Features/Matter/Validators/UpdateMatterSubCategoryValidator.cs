using CourtApp.Application.Features.Matter.Commands;
using FluentValidation;

namespace CourtApp.Application.Features.Matter.Validators;

public class UpdateMatterSubCategoryValidator
    : AbstractValidator<UpdateMatterSubCategoryCommand>
{
    public UpdateMatterSubCategoryValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty();

        this.ApplyMatterSubCategoryRules();
    }
}