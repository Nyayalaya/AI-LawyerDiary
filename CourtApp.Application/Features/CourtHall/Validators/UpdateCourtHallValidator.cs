using CourtApp.Application.Features.CourtHall.Commands;
using FluentValidation;

namespace CourtApp.Application.Features.CourtHall.Validators
{
    public class UpdateCourtHallValidator : AbstractValidator<UpdateCourtHallCommand>
    {
        public UpdateCourtHallValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Court Hall ID is required.");

            RuleFor(x => x.CourtComplexId)
                .NotEmpty().WithMessage("Court Complex ID is required.");

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Hall name is required.")
                .Length(1, 200).WithMessage("Hall name must be between 1 and 200 characters.");

            RuleFor(x => x.Code)
                .NotEmpty().WithMessage("Hall code is required.")
                .Length(1, 50).WithMessage("Hall code must be between 1 and 50 characters.");
        }
    }
}
