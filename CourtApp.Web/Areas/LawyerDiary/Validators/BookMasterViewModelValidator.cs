using CourtApp.Web.Areas.LawyerDiary.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourtApp.Web.Areas.LawyerDiary.Validators
{
    public class BookMasterViewModelValidator : AbstractValidator<BookMasterViewModel>
    {
        public BookMasterViewModelValidator()
        {
            RuleFor(p => p.BookTypeId)
               .NotEmpty().WithMessage("{PropertyName} is required.")
               .NotNull();

            RuleFor(p => p.PublisherId)
              .NotEmpty().WithMessage("{PropertyName} is required.")
              .NotNull();

            RuleFor(p => p.Year)
             .NotEmpty().WithMessage("{PropertyName} is required.")
             .NotNull();

        }
    }
}
