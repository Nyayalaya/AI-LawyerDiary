using AutoMapper;
using CourtApp.Application.Features.CaseStage.Query;
using CourtApp.Application.Features.CaseStages.Command;
using CourtApp.Application.Features.CaseStages.Query;
using CourtApp.Domain.Entities.Masters;

namespace CourtApp.Application.Mappings
{
    public class CaseStageProfile:Profile
    {
        public CaseStageProfile()
        {
            CreateMap<CaseStageQueryByIdResponse, CaseStageEntity>().ReverseMap();
            CreateMap<DeleteCaseStageCommand, CaseStageEntity>().ReverseMap();

            CreateMap<CaseStageEntity, CaseStageResponse>();
            CreateMap<CreateCaseStageCommand, CaseStageEntity>();
            CreateMap<UpdateCaseStageCommand, CaseStageEntity>();
        }
    }
}
