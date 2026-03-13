using AutoMapper;
using CourtApp.Application.DTOs.CaseTitle;
using CourtApp.Application.DTOs.CaseTitleRes;
using CourtApp.Application.DTOs.DOTypes;
using CourtApp.Application.DTOs.FSTitle;
using CourtApp.Application.Features.CaseTitle;
using CourtApp.Application.Features.DOType;
using CourtApp.Application.Features.FSTitle;
using CourtApp.Domain.Entities.CaseDetails;
using CourtApp.Domain.Entities.LawyerDiary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourtApp.Application.Mappings
{
    public class TitleMapping : Profile
    {
        public TitleMapping()
        {
            CreateMap<FSTitleEntity, FSTitleResponse>();            
            CreateMap<FSTitleEntity, FSTitleByIdResponse>();            
            CreateMap<FSTitleCreateCommand, FSTitleEntity>();            
            CreateMap<FSTitleUpdateCommand, FSTitleEntity>();   
            
            CreateMap<CreateCaseTitleCommand, CaseTitleEntity>();            
            CreateMap<CaseApplicantDetail, CaseApplicantDetailEntity>();            
            CreateMap<CaseApplicantDetailEntity, ApplicantDetailDto>();            
            CreateMap<UpdateCaseTitleCommand, CaseTitleEntity>();            
            CreateMap<CaseTitleEntity, CaseTitleByIdResponse>();
            CreateMap<CaseTitleEntity, CaseTitleResponse>();
            CreateMap<CaseApplicantDetailEntity, ApplicantDetailDto>();

        }
    }
}
