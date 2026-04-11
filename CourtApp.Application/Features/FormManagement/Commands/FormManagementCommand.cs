using CourtApp.Application.Features.FormManagement.DTOs;
using MediatR;
using System;
using System.Collections.Generic;

namespace CourtApp.Application.Features.FormManagement.Commands
{
    // FormMaster Commands
    public class CreateFormMasterCommand : IRequest<Guid>
    {
        public Guid FormTypeId { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
    }

    public class UpdateFormMasterCommand : IRequest<Guid>
    {
        public Guid Id { get; set; }
        public Guid FormTypeId { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
    }

    public class DeleteFormMasterCommand : IRequest<bool>
    {
        public Guid Id { get; set; }
    }

    // FormSubtype Commands
    public class CreateFormSubtypeCommand : IRequest<Guid>
    {
        public Guid FormId { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
    }

    public class UpdateFormSubtypeCommand : IRequest<Guid>
    {
        public Guid Id { get; set; }
        public Guid FormId { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
    }

    public class DeleteFormSubtypeCommand : IRequest<bool>
    {
        public Guid Id { get; set; }
    }

    // FormTemplate Commands
    public class CreateFormTemplateCommand : IRequest<Guid>
    {
        public Guid FormSubtypeId { get; set; }
        public string Title { get; set; }
        public string TemplateContent { get; set; }
        public bool IsEditable { get; set; }
        public string Version { get; set; }
        public string CaseTypeCode { get; set; }
        public string StateCode { get; set; }
    }

    public class UpdateFormTemplateCommand : IRequest<Guid>
    {
        public Guid Id { get; set; }
        public Guid FormSubtypeId { get; set; }
        public string Title { get; set; }
        public string TemplateContent { get; set; }
        public bool IsEditable { get; set; }
        public string Version { get; set; }
        public string CaseTypeCode { get; set; }
        public string StateCode { get; set; }
    }

    public class DeleteFormTemplateCommand : IRequest<bool>
    {
        public Guid Id { get; set; }
    }

    // FormTemplateVersion Commands
    public class CreateFormTemplateVersionCommand : IRequest<Guid>
    {
        public Guid FormTemplateId { get; set; }
        public string Content { get; set; }
        public string Version { get; set; }
        public bool IsPublished { get; set; }
    }

    public class UpdateFormTemplateVersionCommand : IRequest<Guid>
    {
        public Guid Id { get; set; }
        public string Content { get; set; }
        public string Version { get; set; }
        public bool IsPublished { get; set; }
    }

    public class DeleteFormTemplateVersionCommand : IRequest<bool>
    {
        public Guid Id { get; set; }
    }

    // FormCaseCategoryMapping Commands
    public class CreateFormCaseCategoryMappingCommand : IRequest<Guid>
    {
        public Guid FormSubtypeId { get; set; }
        public Guid CaseCategoryId { get; set; }
        public bool IsMandatory { get; set; }
    }

    public class UpdateFormCaseCategoryMappingCommand : IRequest<Guid>
    {
        public Guid Id { get; set; }
        public bool IsMandatory { get; set; }
        public bool IsActive { get; set; }
    }

    public class DeleteFormCaseCategoryMappingCommand : IRequest<bool>
    {
        public Guid Id { get; set; }
    }

    // FormCourtMapping Commands
    public class CreateFormCourtMappingCommand : IRequest<Guid>
    {
        public Guid FormSubtypeId { get; set; }
        public Guid CourtTypeId { get; set; }
        public bool IsMandatory { get; set; }
    }

    public class UpdateFormCourtMappingCommand : IRequest<Guid>
    {
        public Guid Id { get; set; }
        public Guid CourtTypeId { get; set; }
        public bool IsMandatory { get; set; }
    }

    public class DeleteFormCourtMappingCommand : IRequest<bool>
    {
        public Guid Id { get; set; }
    }
}
