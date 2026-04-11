using FluentValidation;
using CourtApp.Application.Features.Clients.Queries.SearchClients;

namespace CourtApp.Application.Features.Clients.Validators.SearchClients
{
    public sealed class SearchClientsQueryValidator : AbstractValidator<SearchClientsQuery>
    {
        public SearchClientsQueryValidator()
        {
            RuleFor(x => x.PageNumber)
                .GreaterThan(0).WithMessage("Page number must be greater than 0");

            RuleFor(x => x.PageSize)
                .GreaterThan(0).WithMessage("Page size must be greater than 0")
                .LessThanOrEqualTo(100).WithMessage("Page size cannot exceed 100");

            RuleFor(x => x.SearchTerm)
                .MaximumLength(100).WithMessage("Search term cannot exceed 100 characters")
                .When(x => !string.IsNullOrWhiteSpace(x.SearchTerm));
        }
    }
}
