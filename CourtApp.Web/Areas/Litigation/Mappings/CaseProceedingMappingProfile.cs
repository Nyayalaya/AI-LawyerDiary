using AutoMapper;
using CourtApp.Application.Features.CaseProceeding;
using CourtApp.Web.Areas.Litigation.Models;

namespace CourtApp.Web.Areas.Litigation.Mappings
{
    public class CaseProceedingMappingProfile : Profile
    {
        public CaseProceedingMappingProfile()
        {
            CreateMap<CaseProceedingViewModel, CreateCaseProceedingCommand>();
            CreateMap<CaseProceedingViewModel, UpdateCaseProceedingCommand>();
            CreateMap<GetCaseProceedingQuery, CaseProceedingViewModel>();
            CreateMap<GetCaseProceedingByIdQuery, CaseProceedingViewModel>();

        }
    }
}
