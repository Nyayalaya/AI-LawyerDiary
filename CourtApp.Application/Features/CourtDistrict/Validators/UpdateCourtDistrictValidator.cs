using CourtApp.Application.Features.CourtDistrict.Commands;
using CourtApp.Application.Interfaces.Repositories;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace CourtApp.Application.Features.CourtDistrict.Validators
{
    public class UpdateCourtDistrictValidator : AbstractValidator<UpdateCourtDistrictCommand>
    {
        private readonly ICourtDistrictRepository _repository;

        public UpdateCourtDistrictValidator(ICourtDistrictRepository repository)
        {
            _repository = repository;

            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("District ID is required.");

            RuleFor(x => x.StateId)
                .NotEmpty().WithMessage("State ID is required.")
                .GreaterThan(0).WithMessage("State ID must be greater than 0.");

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("District name is required.")
                .Length(1, 200).WithMessage("District name must be between 1 and 200 characters.");

            // Custom rule to check for duplicate name (excluding current record)
            RuleFor(x => new { x.Name, x.Id, x.StateId })
                .MustAsync(async (nameData, cancellation) =>
                {
                    var normalizedName = nameData.Name.ToLower().Trim();
                    var currentId = nameData.Id;
                    var stateId = nameData.StateId;

                    var exists = await _repository.Entities
                        .Where(w => w.Id != currentId && w.StateId == stateId && w.Name.ToLower() == normalizedName)
                        .FirstOrDefaultAsync(cancellation);

                    return exists == null;
                })
                .WithMessage(x => $"A court district with name '{x.Name}' already exists in this state.")
                .When(x => !string.IsNullOrEmpty(x.Name));

            RuleFor(x => x.Languages)
                .NotNull().WithMessage("Languages list cannot be null.");
        }
    }
}
