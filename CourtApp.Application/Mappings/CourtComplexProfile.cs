using AutoMapper;
using CourtApp.Application.DTOs.CourtComplex;
using CourtApp.Application.Features.CourtComplex;
using CourtApp.Domain.Entities.LawyerDiary;

namespace CourtApp.Application.Mappings
{
    public class CourtComplexProfile : Profile
    {
        public CourtComplexProfile()
        {
            CreateMap<CreateCourtComplexCommand, CourtComplexEntity>()
                /*.ForPath(d => d.DistrictCode, opt => opt.MapFrom(src => src.DistrictId))*/;
            CreateMap<CourtComplexEntity, CourtComplexResponse>();
            CreateMap<CourtComplexEntity, CourtComplexByIdResponse>()
                 /*.ForPath(d => d.DistrictId, opt => opt.MapFrom(src => src.DistrictCode))*/;
        }
    }
}
