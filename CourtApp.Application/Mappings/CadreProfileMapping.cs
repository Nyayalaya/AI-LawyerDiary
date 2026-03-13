using AutoMapper;
using CourtApp.Application.DTOs.Cadre;
using CourtApp.Application.Features.Cadre;
using CourtApp.Application.Features.Case;
using CourtApp.Domain.Entities.LawyerDiary;
namespace CourtApp.Application.Mappings
{
    public class CadreProfileMapping : Profile
    {
        public CadreProfileMapping()
        {
            CreateMap<CadreMasterEntity,GetCadreResponseById>();
            CreateMap<CadreMasterEntity,GetCadreResponse>();
            CreateMap<CadreCreateCommand, CadreMasterEntity>();
            CreateMap<UpdateCadreCommand, CadreMasterEntity>();                   
        }
    }
}
