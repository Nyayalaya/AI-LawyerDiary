using AutoMapper;
using CourtApp.Application.Features.CourtDistrict.Commands;
using CourtApp.Application.Features.CourtDistrict.DTOs;
using CourtApp.Domain.Entities.LawyerDiary;

namespace CourtApp.Application.Mappings
{
    public class CourtDistrictProfile : Profile
    {
        public CourtDistrictProfile()
        {
            CreateMap<CourtDistrictEntity, CourtDistrictByIdReponse>();
            CreateMap<CourtDistrictEntity, CourtDistrictReponse>();
            CreateMap<CreateCourtDistrictCommand, CourtDistrictEntity>();
            CreateMap<UpdateCourtDistrictCommand, CourtDistrictEntity>();
        }
    }
}
