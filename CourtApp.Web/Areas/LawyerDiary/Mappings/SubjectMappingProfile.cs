using AutoMapper;
using CourtApp.Application.Features.Subjects.Commands;
using CourtApp.Application.Features.Subjects.Queries;
using CourtApp.Web.Areas.LawyerDiary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourtApp.Web.Areas.LawyerDiary.Mappings
{
    public  class SubjectMappingProfile : Profile
    {
        public SubjectMappingProfile()
        {
            CreateMap<PracticeSubjectCacheQueryResponse, SubjectViewModel>().ReverseMap();
            CreateMap<PracticeSubjectQueryResponse, SubjectViewModel>().ReverseMap();
            CreateMap<CreateSubjectCommand, SubjectViewModel>().ReverseMap();
            CreateMap<UpdateSubjectCommand, SubjectViewModel>().ReverseMap();
            CreateMap<DeleteSubjectCommand, SubjectViewModel>().ReverseMap();
        }
    }
}
