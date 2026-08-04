using AutoMapper;
using CourtApp.Application.DTOs.Cadre;
using CourtApp.Application.DTOs.Location;
using CourtApp.Application.DTOs.WorkSub;
using CourtApp.Application.Features.Cadre.Commands;
using CourtApp.Application.Features.Cadre.Dtos;
using CourtApp.Application.Features.Court.Commands;
using CourtApp.Application.Features.Court.DTOs;
using CourtApp.Application.Features.CourtHall.Commands;
using CourtApp.Application.Features.CourtHall.DTOs;
using CourtApp.Application.Features.CourtLevel.Query;
using CourtApp.Application.Features.CourtType.Command;
using CourtApp.Application.Features.CourtType.Query;
using CourtApp.Application.Features.FormManagement.Commands;
using CourtApp.Application.Features.FormManagement.DTOs;
using CourtApp.Application.Features.Location;
using CourtApp.Application.Features.State.Query;
using CourtApp.Application.Features.WorkMasterSub.Commands;
using CourtApp.Domain.Entities.Masters;

namespace CourtApp.Application.Mappings
{
    public class AppProfileMapping : Profile
    {
        public AppProfileMapping()
        {
            #region State Profile Mapping
            CreateMap<StateEntity,GetStateMasterResponse>();
            #endregion

            #region Court Type profile Mapping
            CreateMap<GetCourtTypeResponse, CourtTypeEntity>().ReverseMap();
            CreateMap<CreateCourtTypeCommand, CourtTypeEntity>().ReverseMap();
            CreateMap<UpdateCourtTypeCommand, CourtTypeEntity>().ReverseMap();
            CreateMap<DeleteCourtTypeCommand, CourtTypeEntity>().ReverseMap();
            #endregion

            #region Court Level Profile Mapping
            CreateMap<CourtLevelEntity, GetCourtLevelResponse>();
            #endregion

            #region Work & Work Sub Master Profile Mapping
            CreateMap<WorksEntity, WorkSubMasterResponse>()
               .ForMember(dest => dest.WorkId, opt => opt.MapFrom(src => src.WorkId))
               .ForMember(dest => dest.CourtTypeId, opt => opt.MapFrom(src => src.CourtTypeId))
               .ForMember(dest => dest.CourtType, opt => opt.MapFrom(src => src.CourtType.Name))
               .ForMember(dest => dest.WorkName, opt => opt.MapFrom(src => src.Work.Name));
            CreateMap<WorksEntity, WorkSubMasterByIdResponse>()
                .ForMember(dest => dest.CourtType, opt => opt.MapFrom(src => src.CourtType.Name))
                .ForMember(dest => dest.WorkName, opt => opt.MapFrom(src => src.Work.Name));
            CreateMap<CreateWorkSubMasterCommand, WorksEntity>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Works));
            #endregion

            #region Cadre Profile Mapping
            // Map from entity to DTOs
            CreateMap<CadreEntity, CadreResponse>();
            CreateMap<CadreEntity, CadreByIdResponse>();

            // Backward compatibility with old DTOs
            CreateMap<CadreEntity, GetCadreResponseById>();
            CreateMap<CadreEntity, GetCadreResponse>();

            // Map from commands to entity
            CreateMap<CreateCadreCommand, CadreEntity>();
            CreateMap<UpdateCadreCommand, CadreEntity>();
            #endregion

            #region Court Profile Mapping
            CreateMap<CourtEntity, CourtResponse>();
            CreateMap<CourtEntity, CourtByIdResponse>()
                
                .ForMember(dest => dest.CourtTypeName, opt => opt.MapFrom(src => src.CourtType.Name))
                ;
            CreateMap<CreateCourtCommand, CourtEntity>();
            CreateMap<UpdateCourtCommand, CourtEntity>();
            #endregion

            #region Court Hall Profile Mapping
            CreateMap<CreateCourtHallCommand, CourtHallEntity>();
            CreateMap<CourtHallEntity, CourtHallResponse>();
            CreateMap<CourtHallEntity, CourtHallByIdResponse>();
            #endregion

            #region Location Profile Mapping
            CreateMap<CreateLocationCommand.LocationDetail, LocationEntity>();
            CreateMap<LocationEntity, LocationResponse>();
            CreateMap<LocationEntity, LocationByIdResponse>()
                .ForMember(dest => dest.StateName, opt => opt.MapFrom(src => src.State.Name))
                .ForMember(dest => dest.ParentLocationName, opt => opt.MapFrom(src => src.ParentLocation.Name));
            #endregion

            #region FormType Profile Mapping
            CreateMap<FormTypeEntity, FormTypeDto>();
            CreateMap<FormTypeEntity, FormTypeResponseDto>();
            CreateMap<CreateFormTypeDto, FormTypeEntity>();
            CreateMap<UpdateFormTypeDto, FormTypeEntity>();
            #endregion

            #region FormMaster Profile Mapping
            CreateMap<FormMasterEntity, FormMasterDto>().ReverseMap();
            CreateMap<FormMasterEntity, FormMasterResponseDto>()
                .ForMember(dest => dest.FormTypeName, opt => opt.MapFrom(src => src.FormType.Name));
            CreateMap<CreateFormMasterCommand, FormMasterEntity>();
            CreateMap<UpdateFormMasterCommand, FormMasterEntity>();
            CreateMap<CreateFormMasterDto, FormMasterEntity>();
            CreateMap<UpdateFormMasterDto, FormMasterEntity>();
            #endregion

            #region FormSubtype Profile Mapping
            CreateMap<FormSubtypeEntity, FormSubtypeDto>().ReverseMap();
            CreateMap<FormSubtypeEntity, FormSubtypeResponseDto>()
                .ForMember(dest => dest.FormName, opt => opt.MapFrom(src => src.Form.Name));
            CreateMap<CreateFormSubtypeCommand, FormSubtypeEntity>();
            CreateMap<UpdateFormSubtypeCommand, FormSubtypeEntity>();
            CreateMap<CreateFormSubtypeDto, FormSubtypeEntity>();
            CreateMap<UpdateFormSubtypeDto, FormSubtypeEntity>();
            #endregion

            #region FormTemplate Profile Mapping
            CreateMap<FormTemplateEntity, FormTemplateDto>().ReverseMap();
            CreateMap<FormTemplateEntity, FormTemplateResponseDto>()
                .ForMember(dest => dest.FormSubtypeName, opt => opt.MapFrom(src => src.FormSubtype.Name));
            CreateMap<CreateFormTemplateCommand, FormTemplateEntity>();
            CreateMap<UpdateFormTemplateCommand, FormTemplateEntity>();
            CreateMap<CreateFormTemplateDto, FormTemplateEntity>();
            CreateMap<UpdateFormTemplateDto, FormTemplateEntity>();
            #endregion

            #region FormTemplateVersion Profile Mapping
            CreateMap<FormTemplateVersionEntity, FormTemplateVersionDto>().ReverseMap();
            CreateMap<FormTemplateVersionEntity, FormTemplateVersionResponseDto>();
            CreateMap<CreateFormTemplateVersionCommand, FormTemplateVersionEntity>();
            CreateMap<UpdateFormTemplateVersionCommand, FormTemplateVersionEntity>();
            CreateMap<CreateFormTemplateVersionDto, FormTemplateVersionEntity>();
            CreateMap<UpdateFormTemplateVersionDto, FormTemplateVersionEntity>();
            #endregion

            #region FormCaseCategoryMapping Profile Mapping
            CreateMap<FormCaseCategoryMapping, FormCaseCategoryMappingDto>().ReverseMap();
            CreateMap<FormCaseCategoryMapping, FormCaseCategoryMappingResponseDto>();
            CreateMap<CreateFormCaseCategoryMappingCommand, FormCaseCategoryMapping>();
            CreateMap<UpdateFormCaseCategoryMappingCommand, FormCaseCategoryMapping>();
            CreateMap<CreateFormCaseCategoryMappingDto, FormCaseCategoryMapping>();
            CreateMap<UpdateFormCaseCategoryMappingDto, FormCaseCategoryMapping>();
            #endregion

            #region FormCourtMapping Profile Mapping
            CreateMap<FormCourtMapping, FormCourtMappingDto>().ReverseMap();
            CreateMap<FormCourtMapping, FormCourtMappingResponseDto>();
            CreateMap<CreateFormCourtMappingCommand, FormCourtMapping>();
            CreateMap<UpdateFormCourtMappingCommand, FormCourtMapping>();
            CreateMap<CreateFormCourtMappingDto, FormCourtMapping>();
            CreateMap<UpdateFormCourtMappingDto, FormCourtMapping>();
            #endregion
        }
    }
}
