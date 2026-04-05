using AutoMapper;
using CourtApp.Application.Features.CaseCategory.Dto;
using CourtApp.Domain.Entities.Masters;

namespace CourtApp.Application.Mappings
{
    public class CaseNatureProfile:Profile
    {

        public CaseNatureProfile()
        {
            
            CreateMap<CaseCategoryEntity,CaseCategoryResponse>();
            CreateMap<CaseCategoryEntity, CaseCategoryByIdResponse>();
            //CreateMap<Case, NatureEntity>();
            //CreateMap<UpdateCaseNatureCommand, NatureEntity>();
            //CreateMap<DeleteCaseNatureCommand, NatureEntity>();
        }
    }
}
