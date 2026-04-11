using CourtApp.Application.Features.FormManagement.DTOs;
using MediatR;
using System;
using System.Collections.Generic;

namespace CourtApp.Application.Features.FormManagement.Queries
{
    // FormMaster Queries
    public class GetAllFormMastersQuery : IRequest<List<FormMasterResponseDto>> { }

    public class GetFormMasterByIdQuery : IRequest<FormMasterResponseDto>
    {
        public Guid Id { get; set; }
    }

    public class GetFormMasterByCodeQuery : IRequest<FormMasterResponseDto>
    {
        public string Code { get; set; }
    }

    public class GetFormMastersByTypeQuery : IRequest<List<FormMasterResponseDto>>
    {
        public Guid FormTypeId { get; set; }
    }

    // FormSubtype Queries
    public class GetAllFormSubtypesQuery : IRequest<List<FormSubtypeResponseDto>> { }

    public class GetFormSubtypeByIdQuery : IRequest<FormSubtypeResponseDto>
    {
        public Guid Id { get; set; }
    }

    public class GetFormSubtypeByCodeQuery : IRequest<FormSubtypeResponseDto>
    {
        public string Code { get; set; }
    }

    public class GetFormSubtypesByFormQuery : IRequest<List<FormSubtypeResponseDto>>
    {
        public Guid FormId { get; set; }
    }

    // FormTemplate Queries
    public class GetAllFormTemplatesQuery : IRequest<List<FormTemplateResponseDto>> { }

    public class GetFormTemplateByIdQuery : IRequest<FormTemplateResponseDto>
    {
        public Guid Id { get; set; }
    }

    public class GetFormTemplatesBySubtypeQuery : IRequest<List<FormTemplateResponseDto>>
    {
        public Guid FormSubtypeId { get; set; }
    }

    // FormTemplateVersion Queries
    public class GetAllFormTemplateVersionsQuery : IRequest<List<FormTemplateVersionResponseDto>> { }

    public class GetFormTemplateVersionByIdQuery : IRequest<FormTemplateVersionResponseDto>
    {
        public Guid Id { get; set; }
    }

    public class GetFormTemplateVersionsQuery : IRequest<List<FormTemplateVersionResponseDto>>
    {
        public Guid FormTemplateId { get; set; }
    }

    // FormCaseCategoryMapping Queries
    public class GetAllFormCaseCategoryMappingsQuery : IRequest<List<FormCaseCategoryMappingResponseDto>> { }

    public class GetFormCaseCategoryMappingByIdQuery : IRequest<FormCaseCategoryMappingResponseDto>
    {
        public Guid Id { get; set; }
    }

    public class GetFormCaseCategoryMappingsBySubtypeQuery : IRequest<List<FormCaseCategoryMappingResponseDto>>
    {
        public Guid FormSubtypeId { get; set; }
    }

    // FormCourtMapping Queries
    public class GetAllFormCourtMappingsQuery : IRequest<List<FormCourtMappingResponseDto>> { }

    public class GetFormCourtMappingByIdQuery : IRequest<FormCourtMappingResponseDto>
    {
        public Guid Id { get; set; }
    }

    public class GetFormCourtMappingsBySubtypeQuery : IRequest<List<FormCourtMappingResponseDto>>
    {
        public Guid FormSubtypeId { get; set; }
    }
}
