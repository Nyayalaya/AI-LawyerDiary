using AutoMapper;
using CourtApp.Application.Features.CaseKinds.Commands;
using CourtApp.Application.Features.CaseKinds.Query;
using CourtApp.Domain.Entities.LawyerDiary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourtApp.Application.Mappings
{
    public class CaseKindProfile : Profile
    {
        public CaseKindProfile()
        {
            CreateMap<CaseKindEntity,CaseKindQueryByIdResponse>();
            CreateMap<CaseKindCacheQueryResponse, CaseKindEntity>().ReverseMap();
            CreateMap<CreateCaseKindCommand, CaseKindEntity>().ReverseMap();
            CreateMap<UpdateCaseKindCommand, CaseKindEntity>().ReverseMap();
            CreateMap<DeleteCaseKindCommand, CaseKindEntity>().ReverseMap();
        }
    }
}
