using CourtApp.Web.Areas.Admin.Models;
using FluentValidation;

namespace CourtApp.Web.Areas.Admin.Validators
{
    public class UserViewModelValidator : AbstractValidator<UserViewModel>
    {
        public UserViewModelValidator()
        {

            RuleFor(x => x.Role)
            .NotEmpty().WithMessage("Client Type is required.")
            .Must(type => type == "ASSOCIATE" || type == "CLERK")
            .WithMessage("Invalid Role Type selected.");

            RuleFor(x => x.EnrollmentNo)
           .NotEmpty().WithMessage("Enrollment Number is required for Corporate clients.")
           .When(x => x.Role == "ASSOCIATE");

            RuleFor(p => p.FirstName)
               .NotEmpty().WithMessage("{PropertyName} is required.")
               .NotNull();

            RuleFor(p => p.LastName)
               .NotEmpty().WithMessage("{PropertyName} is required.")
               .NotNull();

            RuleFor(x => x.Email)
                .NotEmpty()
                .EmailAddress()
                .WithMessage("Valid Email is required.");

            RuleFor(x => x.Mobile)
          .NotEmpty().WithMessage("Mobile number is required.")
          .Matches(@"^[0-9]{10}$").WithMessage("Enter a valid 10-digit mobile number.");

            // Conditional Validation: EnrollmentNo is required only if UserType is "Lawyer"
            RuleFor(x => x.EnrollmentNo)
                .NotEmpty().WithMessage("Enrollment Number is required for Lawyers.")
                .When(x => x.Role == "LAWYER");

        }
    }
}
