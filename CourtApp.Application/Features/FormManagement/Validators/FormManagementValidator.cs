using CourtApp.Application.Features.FormManagement.Commands;
using FluentValidation;

namespace CourtApp.Application.Features.FormManagement.Validators
{
    public class FormMasterValidator : AbstractValidator<CreateFormMasterCommand>
    {
        public FormMasterValidator()
        {
            RuleFor(x => x.Code).NotEmpty().WithMessage("Code is required");
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required");
            RuleFor(x => x.FormTypeId).NotEmpty().WithMessage("FormTypeId is required");
        }
    }

    public class FormSubtypeValidator : AbstractValidator<CreateFormSubtypeCommand>
    {
        public FormSubtypeValidator()
        {
            RuleFor(x => x.Code).NotEmpty().WithMessage("Code is required");
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required");
            RuleFor(x => x.FormId).NotEmpty().WithMessage("FormId is required");
        }
    }

    public class FormTemplateValidator : AbstractValidator<CreateFormTemplateCommand>
    {
        public FormTemplateValidator()
        {
            RuleFor(x => x.Title).NotEmpty().WithMessage("Title is required");
            RuleFor(x => x.TemplateContent).NotEmpty().WithMessage("Template content is required");
            RuleFor(x => x.FormSubtypeId).NotEmpty().WithMessage("FormSubtypeId is required");
        }
    }

    public class FormTemplateVersionValidator : AbstractValidator<CreateFormTemplateVersionCommand>
    {
        public FormTemplateVersionValidator()
        {
            RuleFor(x => x.Content).NotEmpty().WithMessage("Content is required");
            RuleFor(x => x.Version).NotEmpty().WithMessage("Version is required");
            RuleFor(x => x.FormTemplateId).NotEmpty().WithMessage("FormTemplateId is required");
        }
    }

    public class FormCaseCategoryMappingValidator : AbstractValidator<CreateFormCaseCategoryMappingCommand>
    {
        public FormCaseCategoryMappingValidator()
        {
            RuleFor(x => x.FormSubtypeId).NotEmpty().WithMessage("FormSubtypeId is required");
            RuleFor(x => x.CaseCategoryId).NotEmpty().WithMessage("CaseCategoryId is required");
        }
    }

    public class FormCourtMappingValidator : AbstractValidator<CreateFormCourtMappingCommand>
    {
        public FormCourtMappingValidator()
        {
            RuleFor(x => x.FormSubtypeId).NotEmpty().WithMessage("FormSubtypeId is required");
            RuleFor(x => x.CourtTypeId).NotEmpty().WithMessage("CourtTypeId is required");
        }
    }
}
