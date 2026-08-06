using CourtApp.Application.Features.Matter.Commands.MatterCategory.Create;
using CourtApp.Application.Features.Matter.Extention;
using FluentValidation;

namespace CourtApp.Application.Features.Matter.Validators;

public class CreateMatterCategoryValidator
    : AbstractValidator<CreateMatterCategoryCommand>
{
    public CreateMatterCategoryValidator()
    {
        this.ApplyMatterCategoryRules();
    }
}