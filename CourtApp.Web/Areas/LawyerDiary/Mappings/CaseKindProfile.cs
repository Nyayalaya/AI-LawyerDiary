using AutoMapper;
using CourtApp.Application.Features.CaseKinds.Commands;
using CourtApp.Application.Features.CaseKinds.Query;
using CourtApp.Web.Areas.LawyerDiary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourtApp.Web.Areas.LawyerDiary.Mappings
{
    public class CaseKindProfile:Profile
    {
        public CaseKindProfile()
        {
            CreateMap<CaseKindCacheQueryResponse, CaseKindViewModel>().ReverseMap();
            CreateMap<CaseKindQueryByIdResponse, CaseKindViewModel>().ReverseMap();
            CreateMap<CreateCaseKindCommand, CaseKindViewModel>().ReverseMap();
            CreateMap<UpdateCaseKindCommand, CaseKindViewModel>().ReverseMap();
            CreateMap<DeleteCaseKindCommand, CaseKindViewModel>().ReverseMap();
        }
    }
}
