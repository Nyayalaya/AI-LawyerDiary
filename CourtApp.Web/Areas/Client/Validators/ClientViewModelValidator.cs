using CourtApp.Web.Areas.Client.Model;
using FluentValidation;

namespace CourtApp.Web.Areas.Client.Validators
{
    public class ClientViewModelValidator : AbstractValidator<ClientViewModel>
    {
        public ClientViewModelValidator()
        {
            RuleFor(x => x.ClientType)
            .NotEmpty().WithMessage("Client Type is required.")
            .Must(type => type == "Individual" || type == "Corporate")
            .WithMessage("Invalid Client Type selected.");

            RuleFor(p => p.Name)
               .NotEmpty().WithMessage("Name is required.")
               .NotNull();

            RuleFor(x => x.Mobile)
           .NotEmpty().WithMessage("Mobile number is required.")
           .Matches(@"^[0-9]{10}$").WithMessage("Enter a valid 10-digit mobile number.");

            RuleFor(x => x.Phone)
                .Matches(@"^\+?[0-9]{7,15}$")
                .WithMessage("Enter a valid phone number.")
                .When(x => !string.IsNullOrEmpty(x.Phone)); // Validate only if entered


            RuleFor(p => p.ReferalBy)
               .NotEmpty().WithMessage("{PropertyName} is required.")
               .NotNull();

            RuleFor(p => p.Address)
               .NotEmpty().WithMessage("{PropertyName} is required.")
               .NotNull();

            RuleFor(x => x.RegNo)
            .NotEmpty().WithMessage("Registration Number is required for Corporate clients.")
            .When(x => x.ClientType == "Corporate");
        }
    }
}
