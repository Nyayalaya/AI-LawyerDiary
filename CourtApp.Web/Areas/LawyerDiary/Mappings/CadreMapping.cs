using AutoMapper;
using CourtApp.Application.DTOs.Cadre;
using CourtApp.Application.Features.Cadre;
using CourtApp.Web.Areas.LawyerDiary.Models;

namespace CourtApp.Web.Areas.LawyerDiary.Mappings
{
    public class CadreMapping : Profile
    {
        public CadreMapping()
        {
            CreateMap<GetCadreResponse, CadreMasterViewModel>();
            CreateMap<GetCadreResponseById, CadreMasterViewModel>();            
            CreateMap<CadreMasterViewModel, CadreCreateCommand>();
            CreateMap<CadreMasterViewModel, UpdateCadreCommand>();
            CreateMap<CadreMasterViewModel, DeleteCadreCommand>();
        }
    }
}
