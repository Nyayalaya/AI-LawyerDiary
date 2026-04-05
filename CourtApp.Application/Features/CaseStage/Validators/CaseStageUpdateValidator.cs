using CourtApp.Application.Features.CaseStages.Command;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourtApp.Application.Features.CaseStage.Validators
{
    public sealed class CaseStageUpdateValidator:AbstractValidator<UpdateCaseStageCommand>
    {
        private const string SafeTextPattern = @"^[a-zA-Z0-9\u0600-\u06FF\s\-\.]+$";
        public CaseStageUpdateValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                    .WithMessage("Case stage is required.")
                .MinimumLength(2)
                    .WithMessage("Case stage must be at least 2 characters.")
                .MaximumLength(100)
                    .WithMessage("Case stage must not exceed 100 characters.")
                .Matches(SafeTextPattern)
                    .WithMessage("Case stage must not contain special characters.");

            RuleFor(x => x.Code)
                .NotEmpty()
                    .WithMessage("Code is required.")
                .MinimumLength(1)
                    .WithMessage("Code must be at least 1 character.")
                .MaximumLength(10)
                    .WithMessage("Code must not exceed 10 characters.")
                .Matches(@"^[a-zA-Z0-9]+$")
                    .WithMessage("Code must contain letters and numbers only — no spaces or special characters.");
        }
    }
}
