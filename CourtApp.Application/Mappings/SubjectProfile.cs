using AutoMapper;
using CourtApp.Application.Features.Subjects.Commands;
using CourtApp.Application.Features.Subjects.Queries;
using CourtApp.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourtApp.Application.Mappings
{
    public class SubjectProfile : Profile
    {
        public SubjectProfile()
        {
            CreateMap<CreateSubjectCommand, SubjectEntity>().ReverseMap();           
            CreateMap<UpdateSubjectCommand, SubjectEntity>().ReverseMap();           
            CreateMap<DeleteSubjectCommand, SubjectEntity>().ReverseMap();           
            CreateMap<PracticeSubjectQueryResponse, SubjectEntity>().ReverseMap();
            CreateMap<PracticeSubjectCacheQueryResponse, SubjectEntity>().ReverseMap();
        }
   
    }
}
