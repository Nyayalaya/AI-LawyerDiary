using AutoMapper;
using CourtApp.Application.Features.CaseStages.Command;
using CourtApp.Application.Features.CaseStages.Query;
using CourtApp.Web.Areas.LawyerDiary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourtApp.Web.Areas.LawyerDiary.Mappings
{
    public class CaseStageProfile:Profile
    {
        public CaseStageProfile()
        {
            CreateMap<CaseStageQueryByIdResponse, CaseStageViewModel>().ReverseMap();
            CreateMap<CaseStageCacheAllQueryResponse, CaseStageViewModel>().ReverseMap();
            CreateMap<CreateCaseStageCommand, CaseStageViewModel>().ReverseMap();
            CreateMap<UpdateCaseStageCommand, CaseStageViewModel>().ReverseMap();
            CreateMap<DeleteCaseStageCommand, CaseStageViewModel>().ReverseMap();
           
        }
    }
}
