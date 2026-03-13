using AutoMapper;
using CourtApp.Application.DTOs.CaseCategory;
using CourtApp.Application.Features.CaseCategory;
using CourtApp.Application.Features.CaseNatures.Command;
using CourtApp.Web.Areas.LawyerDiary.Models;

namespace CourtApp.Web.Areas.LawyerDiary.Mappings
{
    public class CaseNatureProfile:Profile
    {
        public CaseNatureProfile()
        {

            CreateMap<CaseCategoryResponse, CaseNatureViewModel>();
            CreateMap<CaseCategoryByIdResponse, CaseNatureViewModel>();            
            CreateMap<CreateCaseNatureCommand, CaseNatureViewModel>().ReverseMap();
            CreateMap<UpdateCaseNatureCommand, CaseNatureViewModel>().ReverseMap();
            CreateMap<DeleteCaseNatureCommand, CaseNatureViewModel>().ReverseMap();
        }
    }
}
