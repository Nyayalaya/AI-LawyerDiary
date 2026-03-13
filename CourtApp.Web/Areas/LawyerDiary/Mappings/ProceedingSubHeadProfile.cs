using AutoMapper;
using CourtApp.Application.DTOs.ProcSubHead;
using CourtApp.Application.Features.ProceedingHead;
using CourtApp.Application.Features.ProceedingSubHead;
using CourtApp.Domain.Entities.LawyerDiary;
using CourtApp.Web.Areas.LawyerDiary.Models;

namespace CourtApp.Web.Areas.LawyerDiary.Mappings
{
    public class ProceedingSubHeadProfile:Profile
    {
        public ProceedingSubHeadProfile()
        {
            CreateMap<GetProcSubHeadResponse, ProceedingSubHeadViewModel>();
            CreateMap<GetProcSubHeadByIdResponse, ProceedingSubHeadViewModel>();
            CreateMap<ProceedingSubHeadViewModel,CreateProcSubHeadCommand>();
            CreateMap<ProceedingSubHeadViewModel,UpdateProcSubHeadCommand>();
            CreateMap<ProceedingSubHeadViewModel,DeleteProcSubHeadCommand>();
            CreateMap<ProcHead, ProceedingHead>();
        }
    }
}
