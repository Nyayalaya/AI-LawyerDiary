using AutoMapper;
using CourtApp.Application.DTOs.CaseCategory;
using CourtApp.Application.DTOs.CaseDetails;
using CourtApp.Application.DTOs.CaseTitle;
using CourtApp.Application.DTOs.DOTypes;
using CourtApp.Application.Features.CaseStages.Query;
using CourtApp.Web.Areas.Litigation.Models;
using System.Collections.Generic;


namespace CourtApp.Web.Areas.Litigation.Mappings
{
    public class CaseSearchMappingProfile : Profile
    {
        public CaseSearchMappingProfile()
        {
            #region Dropdwon Mapping
            CreateMap<DOTypeResponse, DropDownGViewModel>()
                    .ForPath(d => d.Id, s => s.MapFrom(m => m.Id))
                    .ForPath(d => d.Name, s => s.MapFrom(m => m.Name_En));

            CreateMap<CaseStageCacheAllQueryResponse, DropDownGViewModel>()
                    .ForPath(d => d.Id, s => s.MapFrom(m => m.Id))
                    .ForPath(d => d.Name, s => s.MapFrom(m => m.CaseStage));  
            
            CreateMap<CaseCategoryResponse, DropDownGViewModel>()
                    .ForPath(d => d.Id, s => s.MapFrom(m => m.Id))
                    .ForPath(d => d.Name, s => s.MapFrom(m => m.Name_En));

            CreateMap<CaseTitleResponse, DropDownGViewModel>()
                    .ForPath(d => d.Id, s => s.MapFrom(m => m.Id))
                    .ForPath(d => d.Name, s => s.MapFrom(m => m.Title));
            #endregion


            #region Search Response
            CreateMap<GetCaseSearchResponse, CaseSearchViewModel>();
            #endregion
        }
    }
}
