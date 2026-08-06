using CourtApp.Application.Features.DynamicProperty.Commands;
using FluentValidation;

namespace CourtApp.Application.Features.DynamicProperty.Validators;

public class CreateDynamicPropertyValidator : AbstractValidator<CreateDynamicPropertyCommand>
{
    public CreateDynamicPropertyValidator()
    {
        RuleFor(x => x.EntityType).NotEmpty().MaximumLength(100);
        RuleFor(x => x.PropertyCode).NotEmpty().MaximumLength(50);
        RuleFor(x => x.PropertyName).NotEmpty().MaximumLength(200);
        RuleFor(x => x.DisplayOrder).GreaterThanOrEqualTo(0);
        RuleFor(x => x.MaxLength).GreaterThan(0).When(x => x.MaxLength.HasValue);
        RuleFor(x => x.MinValue).LessThanOrEqualTo(x => x.MaxValue).When(x => x.MinValue.HasValue && x.MaxValue.HasValue);
    }
}
