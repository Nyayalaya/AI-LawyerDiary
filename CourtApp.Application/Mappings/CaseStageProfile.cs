using AutoMapper;
using CourtApp.Application.Features.CaseStages.Command;
using CourtApp.Application.Features.CaseStages.Query;
using CourtApp.Domain.Entities.LawyerDiary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourtApp.Application.Mappings
{
    public class CaseStageProfile:Profile
    {
        public CaseStageProfile()
        {
            CreateMap<CaseStageEntity,CaseStageCacheAllQueryResponse>();
            CreateMap<CaseStageQueryByIdResponse, CaseStageEntity>().ReverseMap();
            CreateMap<CreateCaseStageCommand, CaseStageEntity>().ReverseMap();
            CreateMap<UpdateCaseStageCommand, CaseStageEntity>().ReverseMap();
            CreateMap<DeleteCaseStageCommand, CaseStageEntity>().ReverseMap();
        }
    }
}
