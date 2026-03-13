using CourtApp.Web.Areas.LawyerDiary.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourtApp.Web.Areas.LawyerDiary.Validators
{
    public class PublicationViewModelValidator : AbstractValidator<PublisherViewModel>
    {
        public PublicationViewModelValidator()
        {
            RuleFor(p => p.PublicationName)
               .NotEmpty().WithMessage("{PropertyName} is required.")
               .NotNull();

            RuleFor(p => p.PropriatorName)
              .NotEmpty().WithMessage("{PropertyName} is required.")
              .NotNull();         

        }
    }
}
