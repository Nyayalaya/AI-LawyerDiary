using CourtApp.Application.Features.CourtDistrict.Commands;
using CourtApp.Application.Interfaces.Repositories;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace CourtApp.Application.Features.CourtDistrict.Validators
{
    public class CreateCourtDistrictValidator : AbstractValidator<CreateCourtDistrictCommand>
    {
        private readonly ICourtDistrictRepository _repository;

        public CreateCourtDistrictValidator(ICourtDistrictRepository repository)
        {
            _repository = repository;

            RuleFor(x => x.createRequestData)
                .NotEmpty().WithMessage("At least one court district must be provided.")
                .Must(d => d != null && d.Count > 0).WithMessage("District list cannot be empty.");

            RuleForEach(x => x.createRequestData).ChildRules(district =>
            {
                district.RuleFor(d => d.StateId)
                    .NotEmpty().WithMessage("State ID is required.")
                    .GreaterThan(0).WithMessage("State ID must be greater than 0.");

                district.RuleFor(d => d.Name)
                    .NotEmpty().WithMessage("District name is required.")
                    .Length(1, 200).WithMessage("District name must be between 1 and 200 characters.");

              
                // Custom rule to check for duplicate name within the same state
                district.RuleFor(d => new { d.Name, d.StateId })
                    .MustAsync(async (nameState, cancellation) =>
                    {
                        var normalizedName = nameState.Name.ToLower().Trim();
                        var stateId = nameState.StateId;

                        var exists = await _repository.Entities
                            .Where(w => w.StateId == stateId && w.Name.ToLower() == normalizedName)
                            .FirstOrDefaultAsync(cancellation);

                        return exists == null;
                    })
                    .WithMessage(d => $"A court district with name '{d.Name}' already exists in this state.");
            });

           
        }
    }
}
