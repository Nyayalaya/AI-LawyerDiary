

using CourtApp.Application.Features.Matter.Commands;
using CourtApp.Application.Features.Matter.Extention;
using FluentValidation;

public class CreateMatterTypeValidator : AbstractValidator<CreateMatterTypeCommand>
{
    public CreateMatterTypeValidator()
    {
        this.ApplyMatterTypeRules();
    }
}