using AutoMapper;
using CourtApp.Application.DTOs.CourtDistrict;
using CourtApp.Application.Features.CourtDistrict;
using CourtApp.Web.Areas.LawyerDiary.Models;

namespace CourtApp.Web.Areas.LawyerDiary.Mappings
{
    public class CourtDistrictMapping : Profile
    {
        public CourtDistrictMapping()
        {
            CreateMap<CourtDistrictReponse, CourtDistrictViewModel>();
            CreateMap<CourtDistrictByIdReponse, CourtDistrictViewModel>()
                /*.ForPath(d => d.DistrictId, sr => sr.MapFrom(s => s.DistrictCode))*/;
            CreateMap<CourtDistrictViewModel, CreateCourtDistrictCommand>();
            CreateMap<CourtDistrictViewModel, UpdateCourtDistrictCommand>();
            CreateMap<CourtDistrict, StateCourtDistrict>();
        }
    }
}
