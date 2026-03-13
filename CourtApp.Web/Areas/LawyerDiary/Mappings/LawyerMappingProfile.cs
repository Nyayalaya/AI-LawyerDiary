using AutoMapper;
using CourtApp.Application.DTOs.Lawyer;
using CourtApp.Application.Features.Lawyer;
using CourtApp.Web.Areas.LawyerDiary.Models.Lawyer;

namespace CourtApp.Web.Areas.LawyerDiary.Mappings
{
    public class LawyerMappingProfile : Profile
    {
        public LawyerMappingProfile()
        {
            CreateMap<LawyerResponse, LawyerLViewModel>();
            CreateMap<LawyerResponseById, LawyerUpsertViewModel>();
            CreateMap<LawyerUpsertViewModel, LawyerCreateCommand>()
                .ForPath(d => d.RelatedPerson, sr => sr.MapFrom(s => s.RelatedPerson));
            //.ForPath(d => d., sr => sr.MapFrom(s => s.RelatedPerson))
            CreateMap<LawyerUpsertViewModel, LawyerUpdateCommand>();
        }
    }
}
