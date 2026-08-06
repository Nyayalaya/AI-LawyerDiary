using CourtApp.Application.Features.Matter.Interfaces;
using FluentValidation;

namespace CourtApp.Application.Features.Matter.Validators;

public static class MatterSubCategoryValidatorExtensions
{
    public static void ApplyMatterSubCategoryRules<T>(this AbstractValidator<T> validator)
        where T : IMatterSubCategoryRequest
    {
        validator.RuleFor(x => x.MatterCategoryId)
            .NotEmpty();

        validator.RuleFor(x => x.Code)
            .NotEmpty()
            .MaximumLength(20);

        validator.RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(100);

        validator.RuleFor(x => x.Description)
            .MaximumLength(500);

        validator.RuleFor(x => x.DisplayOrder)
            .GreaterThanOrEqualTo(0);
    }
}