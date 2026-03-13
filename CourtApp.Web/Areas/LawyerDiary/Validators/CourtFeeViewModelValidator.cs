using CourtApp.Web.Areas.LawyerDiary.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourtApp.Web.Areas.LawyerDiary.Validators
{
    public class CourtFeeViewModelValidator : AbstractValidator<CourtFeeStructureViewModel>
    {
        public CourtFeeViewModelValidator()
        {
            // RuleFor(p => p.CourtFeeTypeId)
            //     .NotEmpty().WithMessage("{PropertyName} is required.")
            //     .NotNull();

               
           
        }
    }
}
