using System;
using CourtApp.Application.Features.CaseDetails.Dtos;
using FluentValidation;

namespace CourtApp.Application.Features.CaseDetails.Validators
{
    public class AgainstCaseValidator
        : AbstractValidator<CaseAgainstRequestDto>
    {
        public AgainstCaseValidator()
        {
            RuleFor(x => x.CourtTypeId)
                .NotEmpty()
                .WithMessage("Against Case Court Type is required.");

            RuleFor(x => x.CaseCategoryId)
                .NotEmpty()
                .WithMessage("Against Case Category is required.");

            RuleFor(x => x.CaseTypeId)
                .NotEmpty()
                .WithMessage("Against Case Type is required.");

            RuleFor(x => x.CourtId)
                .NotEmpty()
                .WithMessage("Against Case Court is required.");

            RuleFor(x => x.CourtHallId)
                .NotEmpty()
                .WithMessage("Against Case Court Hall is required.");

            RuleFor(x => x.CaseNo)
                .NotEmpty()
                .WithMessage("Against Case Number is required.")
                .MaximumLength(50);

            RuleFor(x => x.CaseYear)
                .InclusiveBetween(1900, DateTime.Now.Year + 1);

            RuleFor(x => x.ImpugedOrderDate)
                .LessThanOrEqualTo(DateTime.Today)
                .When(x => x.ImpugedOrderDate != default(DateTime));

            RuleFor(x => x.CisYear)
                .InclusiveBetween(1900, DateTime.Now.Year + 1)
                .When(x => x.CisYear.HasValue);
        }
    }
}
