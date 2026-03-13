using AutoMapper;
using CourtApp.Application.DTOs.Case;
using CourtApp.Application.DTOs.CaseProceedings;
using CourtApp.Application.DTOs.ProcSubHead;
using CourtApp.Application.Features.CaseProceeding;
using CourtApp.Application.Features.ProceedingHead;
using CourtApp.Web.Areas.LawyerDiary.Models;
using CourtApp.Web.Areas.Litigation.Models;

namespace CourtApp.Web.Areas.Litigation.Mappings
{
    public class HearingMappingProfile:Profile
    {
        public HearingMappingProfile()
        {
            CreateMap<CaseDetailResponse, HearingViewModel>();           
            CreateMap<CaseProceedingViewModel, CreateCaseProceedingCommand>();           
            CreateMap<CaseWorkingViewModel, ProceedingWorkDto>().ReverseMap();           
            CreateMap<ProcWork, PrWork>().ReverseMap();           
            CreateMap<CaseProceedingDto, CaseProceedingViewModel>().ReverseMap();  
            CreateMap<GetProcSubHeadResponse, ProceedingSubHeadViewModel>().ReverseMap();  
            CreateMap<CaseProceedingViewModel, UpdateProceedingHeadCommand>().ReverseMap();  
            
                       
        }
    }
}
