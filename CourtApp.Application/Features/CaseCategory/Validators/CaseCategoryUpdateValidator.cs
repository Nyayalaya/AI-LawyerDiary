using CourtApp.Application.Features.CaseCategory.Commands;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourtApp.Application.Features.CaseCategory.Validators
{
    public class CaseCategoryUpdateValidator:AbstractValidator<CaseCategoryUpdateCommand>
    {
        public CaseCategoryUpdateValidator() 
        { 

            RuleFor(x => x.Id).NotEmpty().WithMessage("Id is required.");
            RuleFor(x => x.Name_En).NotEmpty().WithMessage("Name in English is required.");
        }
    }
}
