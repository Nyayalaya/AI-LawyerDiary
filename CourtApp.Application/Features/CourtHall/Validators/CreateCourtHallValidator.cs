using CourtApp.Application.Features.CourtHall.Commands;
using CourtApp.Application.Features.CourtHall.Interfaces;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace CourtApp.Application.Features.CourtHall.Validators
{
    public class CreateCourtHallValidator : AbstractValidator<CreateCourtHallCommand>
    {
        private readonly ICourtHallRepository _repository;

        public CreateCourtHallValidator(ICourtHallRepository repository)
        {
            _repository = repository;

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

                hall.RuleFor(h => h.JudgeName)
                    .Length(0, 200).WithMessage("Judge name must not exceed 200 characters.");

                hall.RuleFor(h => h.RoomNumber)
                    .Length(0, 50).WithMessage("Room number must not exceed 50 characters.");

                // Custom rule to check for duplicate name within the same complex
                hall.RuleFor(d => new { d.Name, complexId = ((CreateCourtHallCommand)(object)this).CourtComplexId })
                    .MustAsync(async (nameData, cancellation) =>
                    {
                        var normalizedName = nameData.Name.ToLower().Trim();
                        var complexId = nameData.complexId;

                        var exists = await _repository.Entities
                            .Where(w => w.CourtComplexId == complexId && w.Name.ToLower() == normalizedName)
                            .FirstOrDefaultAsync(cancellation);

                        return exists == null;
                    })
                    .WithMessage(d => $"A court hall with name '{d.Name}' already exists in this complex.");
            });
        }
    }
}
