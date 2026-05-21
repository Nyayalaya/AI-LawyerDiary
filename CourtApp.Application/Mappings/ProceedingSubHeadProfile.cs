using AutoMapper;
using CourtApp.Application.DTOs.ProceedingHead;
using CourtApp.Application.DTOs.ProcSubHead;
using CourtApp.Application.Features.ProceedingHead;
using CourtApp.Application.Features.ProceedingSubHead;
using CourtApp.Domain.Entities.Masters;
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
            CreateMap<ProceedingEntity, GetProcSubHeadResponse>();            
            CreateMap<ProceedingEntity, GetProcSubHeadByIdResponse>();
            CreateMap<CreateProcSubHeadCommand, ProceedingEntity>();
            CreateMap<UpdateProcSubHeadCommand, ProceedingEntity>();
            CreateMap<DeleteProcSubHeadCommand, ProceedingEntity>();
           
        }
    }
}
