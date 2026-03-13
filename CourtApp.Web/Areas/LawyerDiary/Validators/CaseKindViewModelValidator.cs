using CourtApp.Web.Areas.LawyerDiary.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourtApp.Web.Areas.LawyerDiary.Validators
{
    public class CaseKindViewModelValidator : AbstractValidator<CaseKindViewModel>
    {
        public CaseKindViewModelValidator()
        {
            RuleFor(p => p.CaseKind)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .MaximumLength(50).WithMessage("{PropertyName} must not exceed 50 characters.");
           
        }
    }
}
