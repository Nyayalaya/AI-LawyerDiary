using AutoMapper;
using CourtApp.Application.DTOs.ProceedingHead;
using CourtApp.Application.DTOs.ProcSubHead;
using CourtApp.Application.Features.ProceedingHead;
using CourtApp.Application.Features.ProceedingSubHead;
using CourtApp.Domain.Entities.LawyerDiary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourtApp.Application.Mappings
{
    public class ProceedingSubHeadProfile: Profile
    {
        public ProceedingSubHeadProfile()
        {
            CreateMap<ProceedingSubHeadEntity, GetProcSubHeadResponse>();            
            CreateMap<ProceedingSubHeadEntity, GetProcSubHeadByIdResponse>();
            CreateMap<CreateProcSubHeadCommand, ProceedingSubHeadEntity>();
            CreateMap<UpdateProcSubHeadCommand, ProceedingSubHeadEntity>();
            CreateMap<DeleteProcSubHeadCommand, ProceedingSubHeadEntity>();
           
        }
    }
}
