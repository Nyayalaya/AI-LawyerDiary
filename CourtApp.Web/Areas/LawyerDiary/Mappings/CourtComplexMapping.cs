using AutoMapper;
using CourtApp.Application.DTOs.CourtComplex;
using CourtApp.Application.DTOs.CourtDistrict;
using CourtApp.Application.Features.CourtComplex;
using CourtApp.Application.Features.CourtDistrict;
using CourtApp.Web.Areas.LawyerDiary.Models;

namespace CourtApp.Web.Areas.LawyerDiary.Mappings
{
    public class CourtComplexMapping:Profile
    {
        public CourtComplexMapping()
        {
            CreateMap<CourtComplexResponse, CourtComplexViewModel>();
            CreateMap<CourtComplexByIdResponse, CourtComplexViewModel>();
            CreateMap<CourtComplexViewModel, CreateCourtComplexCommand>();
            CreateMap<Complex, DistrictComplex>();
            CreateMap<CourtComplexViewModel, UpdateCourtComplexCommand>();
        }
    }
}
