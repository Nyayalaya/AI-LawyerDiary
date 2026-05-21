using CourtApp.Application.Features.CourtDistrict.Commands;
using CourtApp.Application.Interfaces.Repositories;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace CourtApp.Application.Features.CourtDistrict.Validators
{
    public class UpdateCourtDistrictBatchValidator : AbstractValidator<UpdateCourtDistrictBatchCommand>
    {
        private readonly ICourtDistrictRepository _repository;

        public UpdateCourtDistrictBatchValidator(ICourtDistrictRepository repository)
        {
            _repository = repository;

            RuleFor(x => x.UpdateItems)
                .NotEmpty().WithMessage("At least one court district must be provided for update.")
                .Must(d => d != null && d.Count > 0).WithMessage("Update items list cannot be empty.");

            RuleForEach(x => x.UpdateItems).ChildRules(district =>
            {
                district.RuleFor(d => d.Id)
                    .NotEmpty().WithMessage("District ID is required.");

                district.RuleFor(d => d.StateId)
                    .NotEmpty().WithMessage("State ID is required.")
                    .GreaterThan(0).WithMessage("State ID must be greater than 0.");

                district.RuleFor(d => d.Name)
                    .NotEmpty().WithMessage("District name is required.")
                    .Length(1, 200).WithMessage("District name must be between 1 and 200 characters.");

                // Custom rule to check for duplicate name (excluding current record being updated)
                district.RuleFor(d => new { d.Name, d.Id, d.StateId })
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
                    .WithMessage(d => $"A court district with name '{d.Name}' already exists.");
            });

            RuleFor(x => x.Languages)
                .NotNull().WithMessage("Languages list cannot be null.");
        }
    }
}
