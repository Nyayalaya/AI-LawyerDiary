using AutoMapper;
using CourtApp.Application.Features.Typeofcasess.Commands;
using CourtApp.Application.Features.Typeofcasess.Query;
using CourtApp.Web.Areas.LawyerDiary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourtApp.Web.Areas.LawyerDiary.Mappings
{
    public class TypeofcasesProfile:Profile
    {
        public TypeofcasesProfile()
        {
            CreateMap<TypeOfCasesQueryByIdResponse, TypeOfCasesViewModel>().ReverseMap();
            CreateMap<GetAllTypeOfCasesResponse, TypeOfCasesViewModel>().ReverseMap();
            CreateMap<TypeOfCasesCacheQueryResponse, TypeOfCasesViewModel>().ReverseMap();
            CreateMap<CreateTypeOfCasesCommand, TypeOfCasesViewModel>().ReverseMap();
            CreateMap<UpdateTypeOfCasesCommand, TypeOfCasesViewModel>().ReverseMap();
            CreateMap<DeleteTypeOfCasesCommand, TypeOfCasesViewModel>().ReverseMap();
            CreateMap<CaseType, TypeOfCase>();
        }
    }
}
