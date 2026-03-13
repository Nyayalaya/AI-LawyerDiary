using AutoMapper;
using CourtApp.Application.DTOs.Case;
using CourtApp.Application.DTOs.CaseDetails;
using CourtApp.Application.Features.Case;
using CourtApp.Application.Features.CaseDetails;
using CourtApp.Domain.Entities.CaseDetails;

namespace CourtApp.Application.Mappings
{
    public class CaseMappingProfile : Profile
    {
        public CaseMappingProfile()
        {


            CreateMap<CreateCaseCommand, CaseDetailEntity>();
            CreateMap<UpdateCaseDetailCommand, CaseDetailEntity>();
            CreateMap<CaseAgainstEntityModel, CaseDetailAgainstEntity>();
            CreateMap<CaseDetailEntity, CaseDetailResponse>();
            CreateMap<CaseDetailEntity, UserCaseDetailResponse>();
            CreateMap<CaseDetailEntity, CaseDetailInfoDto>();
            CreateMap<CreateCaseAssignedCommand, AssignCaseEntity>();
        }
    }
}
