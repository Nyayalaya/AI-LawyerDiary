using CourtApp.Web.Areas.Client.Model;
using FluentValidation;
namespace CourtApp.Web.Areas.LawyerDiary.Validators
{
    public class ClientsViewModelValidator : AbstractValidator<ClientViewModel>
    {
        public ClientsViewModelValidator()
        {
            //RuleFor(p => p.FirstName)
            //    .NotEmpty().WithMessage("{PropertyName} is required.")
            //    .NotNull()
            //    .MaximumLength(50).WithMessage("{PropertyName} must not exceed 50 characters.");

            //RuleFor(p => p.FatherName)
            //    .NotEmpty().WithMessage("{PropertyName} is required.")
            //    .NotNull()
            //    .MaximumLength(50).WithMessage("{PropertyName} must not exceed 50 characters.");

            //RuleFor(p => p.LastName)
            //    .NotEmpty().WithMessage("{PropertyName} is required.")
            //    .NotNull()
            //    .MaximumLength(50).WithMessage("{PropertyName} must not exceed 50 characters.");

            RuleFor(p => p.Email)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .MaximumLength(50).WithMessage("{PropertyName} must not exceed 50 characters.");

            RuleFor(p => p.OfficeEmail)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .MaximumLength(50).WithMessage("{PropertyName} must not exceed 50 characters.");

            RuleFor(p => p.Phone)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .MaximumLength(20).WithMessage("{PropertyName} must not exceed 20 characters.");

            RuleFor(p => p.Mobile)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .MaximumLength(15).WithMessage("{PropertyName} must not exceed 15 characters.");

            //RuleFor(p => p.LandMark)
            //    .NotEmpty().WithMessage("{PropertyName} is required.")
            //    .NotNull()
            //    .MaximumLength(50).WithMessage("{PropertyName} must not exceed 50 characters.");

            RuleFor(p => p.Address)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .MaximumLength(50).WithMessage("{PropertyName} must not exceed 50 characters.");

            //RuleFor(p => p.CountryCode)
            //    .NotEmpty().WithMessage("{PropertyName} is required.")
            //    .NotNull();

            //RuleFor(p => p.StateCode)
            //    .NotEmpty().WithMessage("{PropertyName} is required.")
            //    .NotNull();

            //RuleFor(p => p.DistrictCode)
            //    .NotEmpty().WithMessage("{PropertyName} is required.")
            //    .NotNull();


        }
    }
}
