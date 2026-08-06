using CourtApp.Application.Features.Court.Commands;
using FluentValidation;

namespace CourtApp.Application.Features.Court.Validators
{
    public class UpdateCourtValidator : AbstractValidator<UpdateCourtCommand>
    {
        public UpdateCourtValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Court ID is required");

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Court name is required")
                .MaximumLength(255).WithMessage("Court name must not exceed 255 characters");

            RuleFor(x => x.LocationId)
                .NotEmpty().WithMessage("Location ID is required");

            RuleFor(x => x.CourtTypeId)
                .NotEmpty().WithMessage("Court type ID is required");

            RuleFor(x => x.CourtLevelId)
                .NotEmpty().WithMessage("Court level ID is required");
        }
    }
}
