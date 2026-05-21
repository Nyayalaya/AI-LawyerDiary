using AutoMapper;
using CourtApp.Application.DTOs.ProceedingHead;
using CourtApp.Application.Features.ProceedingHead;
using CourtApp.Domain.Entities.Masters;

namespace CourtApp.Application.Mappings
{
    public class ProceedingHeadProfile : Profile
    {
        public ProceedingHeadProfile()
        {
            CreateMap<ProceedingTypeEntity, GetProceedingHeadResponse>();
            CreateMap<CreateProceedingHeadCommand, ProceedingTypeEntity>();
            CreateMap<UpdateProceedingHeadCommand, ProceedingTypeEntity>();
            CreateMap<DeleteProceedingHeadCommand, ProceedingTypeEntity>();
            
        }
    }
}
