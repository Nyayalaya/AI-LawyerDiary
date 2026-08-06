using CourtApp.Application.Features.CourtHall.Commands;
using CourtApp.Application.Features.CourtHall.Interfaces;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace CourtApp.Application.Features.CourtHall.Validators
{
    public class UpdateCourtHallValidator : AbstractValidator<UpdateCourtHallCommand>
    {
        private readonly ICourtHallRepository _repository;

        public UpdateCourtHallValidator(ICourtHallRepository repository)
        {
            _repository = repository;

            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Court Hall ID is required.");

            RuleFor(x => x.CourtComplexId)
                .NotEmpty().WithMessage("Court Complex ID is required.");

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Hall name is required.")
                .Length(1, 200).WithMessage("Hall name must be between 1 and 200 characters.");

            RuleFor(x => x.JudgeName)
                .Length(0, 200).WithMessage("Judge name must not exceed 200 characters.");

            RuleFor(x => x.RoomNumber)
                .Length(0, 50).WithMessage("Room number must not exceed 50 characters.");

            // Custom rule to check for duplicate name (excluding current record)
            RuleFor(x => new { x.Name, x.Id, x.CourtComplexId })
                .MustAsync(async (nameData, cancellation) =>
                {
                    var normalizedName = nameData.Name.ToLower().Trim();
                    var currentId = nameData.Id;
                    var complexId = nameData.CourtComplexId;

                    var exists = await _repository.Entities
                        .Where(w => w.Id != currentId && w.CourtComplexId == complexId && w.Name.ToLower() == normalizedName)
                        .FirstOrDefaultAsync(cancellation);

                    return exists == null;
                })
                .WithMessage(x => $"A court hall with name '{x.Name}' already exists in this complex.")
                .When(x => !string.IsNullOrEmpty(x.Name));
        }
    }
}
