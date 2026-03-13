using AutoMapper;
using CourtApp.Application.DTOs.ProceedingHead;
using CourtApp.Application.Features.ProceedingHead;
using CourtApp.Domain.Entities.LawyerDiary;

namespace CourtApp.Application.Mappings
{
    public class ProceedingHeadProfile : Profile
    {
        public ProceedingHeadProfile()
        {
            CreateMap<ProceedingHeadEntity, GetProceedingHeadResponse>();
            CreateMap<CreateProceedingHeadCommand, ProceedingHeadEntity>();
            CreateMap<UpdateProceedingHeadCommand, ProceedingHeadEntity>();
            CreateMap<DeleteProceedingHeadCommand, ProceedingHeadEntity>();
            
        }
    }
}
