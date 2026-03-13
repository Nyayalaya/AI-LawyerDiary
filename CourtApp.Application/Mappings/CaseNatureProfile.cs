using AutoMapper;
using CourtApp.Application.Features.CaseCategory.Dto;
using CourtApp.Application.Features.CaseNatures.Command;
using CourtApp.Domain.Entities.LawyerDiary;

namespace CourtApp.Application.Mappings
{
    public class CaseNatureProfile:Profile
    {

        public CaseNatureProfile()
        {
            
            CreateMap<NatureEntity,CaseCategoryResponse>();
            CreateMap<NatureEntity, CaseCategoryByIdResponse>();
            CreateMap<CreateCaseNatureCommand, NatureEntity>();
            CreateMap<UpdateCaseNatureCommand, NatureEntity>();
            CreateMap<DeleteCaseNatureCommand, NatureEntity>();
        }
    }
}
