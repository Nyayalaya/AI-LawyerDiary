using AutoMapper;
using CourtApp.Application.DTOs.CaseProceedings;
using CourtApp.Application.Features.CaseProceeding;
using CourtApp.Domain.Entities.CaseDetails;
namespace CourtApp.Application.Mappings
{
    public class CaseProceedingProfile : Profile
    {
        public CaseProceedingProfile()
        {
            CreateMap<CreateCaseProceedingCommand, CaseProcedingEntity>().ReverseMap();
            CreateMap<UpdateCaseProceedingCommand, CaseProcedingEntity>().ReverseMap();
            CreateMap<CaseProcedingEntity, CaseProceedingDto>().ReverseMap();
            CreateMap<ProceedingWorkDto, ProceedingWorkEntity>()
                .ForPath(d=>d.LastWorkingDate, opt => opt.MapFrom(src => src.WorkingDate))
                .ForPath(d=>d.Works, opt => opt.MapFrom(src => src.Workdt))
                .ReverseMap();
            CreateMap<ProcWorkEntity, PrWork>().ReverseMap();
        }
    }
}
