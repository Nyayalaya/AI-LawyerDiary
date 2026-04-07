using CourtApp.Application.Features.CourtHall.Commands;
using FluentValidation;

namespace CourtApp.Application.Features.CourtHall.Validators
{
    public class CreateCourtHallValidator : AbstractValidator<CreateCourtHallCommand>
    {
        public CreateCourtHallValidator()
        {
            RuleFor(x => x.CourtComplexId)
                .NotEmpty().WithMessage("Court Complex ID is required.");

            RuleFor(x => x.Halls)
                .NotEmpty().WithMessage("At least one court hall must be provided.")
                .Must(h => h != null && h.Count > 0).WithMessage("Halls list cannot be empty.");

            RuleForEach(x => x.Halls).ChildRules(hall =>
            {
                hall.RuleFor(h => h.Name)
                    .NotEmpty().WithMessage("Hall name is required.")
                    .Length(1, 200).WithMessage("Hall name must be between 1 and 200 characters.");

                hall.RuleFor(h => h.Code)
                    .NotEmpty().WithMessage("Hall code is required.")
                    .Length(1, 50).WithMessage("Hall code must be between 1 and 50 characters.");
            });
        }
    }
}
