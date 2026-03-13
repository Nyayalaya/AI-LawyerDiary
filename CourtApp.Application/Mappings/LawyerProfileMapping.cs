using AutoMapper;
using CourtApp.Application.DTOs.Lawyer;
using CourtApp.Application.Features.Lawyer;
using CourtApp.Domain.Entities.LawyerDiary;

namespace CourtApp.Application.Mappings
{
    public class LawyerProfileMapping : Profile
    {
        public LawyerProfileMapping()
        {
            CreateMap<LawyerCreateCommand, LawyerMasterEntity>()
                .ForPath(d => d.RelPerson, sr => sr.MapFrom(m => m.RelatedPerson))
                ;
            CreateMap<LawyerMasterEntity, LawyerResponseById>()
                .ForPath(d => d.RelatedPerson, sr => sr.MapFrom(m => m.RelPerson))
                .ForPath(d => d.Religion, sr => sr.MapFrom(m => m.Relegion))
                .ForPath(d => d.Dob, sr => sr.MapFrom(m => m.Dob));
            CreateMap<LawyerMasterEntity, LawyerResponse>();
            //.ForPath(d => d.Name,
            //opt => opt.MapFrom(src => src.FirstName + "" + src.MiddleName + "" + src.LastName));
        }
    }
}
