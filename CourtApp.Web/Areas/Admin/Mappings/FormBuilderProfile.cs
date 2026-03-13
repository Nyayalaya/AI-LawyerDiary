using AutoMapper;
using CourtApp.Application.DTOs.FormBuilder;
using CourtApp.Application.Features.FormBuilder;
using CourtApp.Web.Areas.Admin.Models;
using CourtApp.Web.Areas.Litigation.Models;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace CourtApp.Web.Areas.Admin.Mappings
{
    public class FormBuilderProfile : Profile
    {
        public FormBuilderProfile()
        {
            CreateMap<FormBuilderResponseDto, GenFormAttrViewModel>();
            CreateMap<GenerateFormViewModel, CreateFormBuilderCommand>();
            CreateMap<GenerateFormViewModel, UpdateFormBuilderCommand>();
            CreateMap<FormBuilderResponseByIdDto,GenerateFormViewModel>();
            CreateMap<FieldDetailsDto, FormFields>();
            CreateMap<FormViewModel, FormFieldsDto>();
            CreateMap<FormFields, FieldDetailsDto>();
            CreateMap<FieldLength, FieldSizeDto>();
            CreateMap<FormBuilderResponseDto, DropDownGViewModel>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.FormName));

            CreateMap<GetTemplateInfoDtoResponse, DropDownGViewModel>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.TemplateName));

            CreateMap<Mapping, MappingDto>();
            CreateMap<FormTemplateMapViewModel, CreateTemplateFormMappingCommand>();
            
        }
    }
}
