using AutoMapper;
using CourtApp.Application.DTOs.FormBuilder;
using CourtApp.Application.Features.FormBuilder;
using CourtApp.Domain.Entities.FormBuilder;
using System.Text.Json;
namespace CourtApp.Application.Mappings
{
    public class FormBuilderProfile : Profile
    {
        public FormBuilderProfile()
        {
            CreateMap<CreateFormBuilderCommand, FormBuilderEntity>()
                .ForMember(dest => dest.FieldsDetails, opt => opt.MapFrom(src => src.Form));

            CreateMap<FormFieldsDto, FormFieldsEntity>();
            CreateMap<FieldDetailsDto, FieldDetailsEntity>().ReverseMap();
            CreateMap<FieldSizeDto, FieldSizeEntity>().ReverseMap();
            CreateMap<FormBuilderEntity, FormBuilderResponseDto>()
                .ForMember(dest => dest.Fields, opt => opt.MapFrom(src => JsonSerializer.Serialize(src.FieldsDetails, JsonSerializerOptions.Default)));

            CreateMap<FormBuilderEntity, FormBuilderResponseByIdDto>();
            CreateMap<CreateTemplateInfoCommand, TemplateInfoEntity>();

            CreateMap<CreateTemplateFormMappingCommand, FormTemplateMappingEntity>();
            CreateMap<MappingDto, MappingEntity>();
            CreateMap<TemplateTags, TemplateTagsEntity>();

        }
    }

}
