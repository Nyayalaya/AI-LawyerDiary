using CourtApp.Application.Features.CaseDetails;
using CourtApp.Application.Features.CaseDetails.Commands;
using FluentValidation;
using System;
using System.Linq;

namespace CourtApp.Application.Features.CaseDetails.Validators
{
    public class CreateCaseCommandValidator
        : AbstractValidator<CreateCaseCommand>
    {
        public CreateCaseCommandValidator()
        {
            RuleFor(x => x.Case)
                .NotNull()
                .WithMessage("Case details are required.");

            When(x => x.Case != null, () =>
            {
                RuleFor(x => x.Case.CourtTypeId)
                    .NotEmpty()
                    .WithMessage("Court Type is required.");

                RuleFor(x => x.Case.CaseCategoryId)
                    .NotEmpty()
                    .WithMessage("Case Category is required.");

                RuleFor(x => x.Case.FirstTitleId)
                    .NotEmpty()
                    .WithMessage("First Title is required.");

                RuleFor(x => x.Case.SecondTitleId)
                    .NotEmpty()
                    .WithMessage("Second Title is required.");

                RuleFor(x => x.Case.CourtId)
                    .NotEmpty()
                    .WithMessage("Court is required.");

                RuleFor(x => x.Case.CourtHallId)
                    .NotEmpty()
                    .WithMessage("Court Hall is required.");

                RuleFor(x => x.Case.InstitutionDate)
                    .LessThanOrEqualTo(DateTime.Today)
                    .When(x => x.Case.InstitutionDate.HasValue)
                    .WithMessage("Institution date cannot be in the future.");

                RuleFor(x => x.Case.NextDate)
                    .GreaterThanOrEqualTo(x => x.Case.InstitutionDate)
                    .When(x => x.Case.NextDate.HasValue && x.Case.InstitutionDate.HasValue)
                    .WithMessage("Next date must be greater than or equal to institution date.");

                RuleFor(x => x.Case.ParentCaseId)
                    .NotEqual(Guid.Empty)
                    .When(x => x.Case.ParentCaseId.HasValue)
                    .WithMessage("Invalid linked case.");

                RuleFor(x => x.Case.CaseNo)
                    .MaximumLength(50)
                    .When(x => !string.IsNullOrWhiteSpace(x.Case.CaseNo));

                RuleFor(x => x.Case.CaseYear)
                    .InclusiveBetween(1900, DateTime.Now.Year + 1)
                    .When(x => x.Case.CaseYear > 0);

                RuleFor(x => x.Case.CisNumber)
                    .MaximumLength(100);

                RuleFor(x => x.Case.CnrNumber)
                    .MaximumLength(100);
            });

            RuleForEach(x => x.Case.AgainstCases)
                 .SetValidator(new AgainstCaseValidator())
                 .When(x => x.Case.AgainstCases != null &&
                            x.Case.AgainstCases.Any());
        }
    }
}
