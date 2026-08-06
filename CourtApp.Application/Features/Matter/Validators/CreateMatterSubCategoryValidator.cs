using CourtApp.Application.Features.Matter.Commands;
using FluentValidation;

namespace CourtApp.Application.Features.Matter.Validators;

public class CreateMatterSubCategoryValidator
    : AbstractValidator<CreateMatterSubCategoryCommand>
{
    public CreateMatterSubCategoryValidator()
    {
        this.ApplyMatterSubCategoryRules();
    }
}