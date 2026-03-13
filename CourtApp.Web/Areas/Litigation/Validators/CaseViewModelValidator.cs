using CourtApp.Web.Areas.Litigation.Models;
using FluentValidation;

namespace CourtApp.Web.Areas.Litigation.Validators
{
    public class CaseViewModelValidator : AbstractValidator<CaseUpseartViewModel>
    {
        public CaseViewModelValidator()
        {

            RuleFor(p => p.ClientId.Value)
                .NotEmpty().WithMessage("Client detail is required.")
                .NotNull();
            RuleFor(p => p.AppearenceID)
               .NotEmpty().WithMessage("Appearence detail is required.")
               .NotNull();

            RuleFor(p => p.InstitutionDate)
                .NotEmpty().WithMessage("Institution date is required.")
                .NotNull();
            RuleFor(p => p.StateId)
                .NotEmpty().WithMessage("State is required.")
                .NotNull();

            RuleFor(p => p.CourtTypeId)
                .NotEmpty().WithMessage("Court type is required.")
                .NotNull();

            RuleFor(p => p.CaseCategoryId)
              .NotEmpty().WithMessage("Case category is required.")
              .NotNull();

            RuleFor(p => p.CaseTypeId)
              .NotEmpty().WithMessage("Case type is required.")
              .NotNull();

            RuleFor(p => p.FirstTitle)
              .NotEmpty().WithMessage("First title is required.")
              .NotNull();

            RuleFor(p => p.FTitleId)
              .NotEmpty().WithMessage("First title,title is required.")
              .NotNull();

            RuleFor(p => p.SecondTitle)
             .NotEmpty().WithMessage("Second title is required.")
             .NotNull();

            RuleFor(p => p.STitleId)
              .NotEmpty().WithMessage("Second title, title is required.")
              .NotNull();

            RuleFor(p => p.CaseStageId)
              .NotEmpty().WithMessage("Case stage is required.")
              .NotNull();
        }
    }
}
