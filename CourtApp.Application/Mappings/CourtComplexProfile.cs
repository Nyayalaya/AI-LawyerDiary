using AutoMapper;
using CourtApp.Application.DTOs.CourtComplex;
using CourtApp.Application.Features.CourtComplex;
using CourtApp.Domain.Entities.Masters;

namespace CourtApp.Application.Mappings
{
    public class CourtComplexProfile : Profile
    {
        public CourtComplexProfile()
        {
            CreateMap<CreateCourtComplexCommand, CourtComplexEntity>();
            CreateMap<CourtComplexEntity, CourtComplexResponse>();
            CreateMap<CourtComplexEntity, CourtComplexByIdResponse>();
        }
    }
}
