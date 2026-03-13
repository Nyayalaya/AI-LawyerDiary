using AutoMapper;
using CourtApp.Application.DTOs.ProceedingHead;
using CourtApp.Application.Features.ProceedingHead;
using CourtApp.Web.Areas.LawyerDiary.Models;

namespace CourtApp.Web.Areas.LawyerDiary.Mappings
{
    public class ProceedingHeadProfile:Profile
    {
        public ProceedingHeadProfile()
        {  
            CreateMap<GetProceedingHeadResponse, ProceedingHeadViewModel>();
            CreateMap<ProceedingHeadViewModel,CreateProceedingHeadCommand>();
            CreateMap<ProceedingHeadViewModel,UpdateProceedingHeadCommand>();
            
        }
    }
}
