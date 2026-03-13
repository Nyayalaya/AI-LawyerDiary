using AutoMapper;
using CourtApp.Application.Features.ProceedingHead;
using CourtApp.Application.Features.WorkMaster;
using CourtApp.Domain.Entities.LawyerDiary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourtApp.Application.Mappings
{
    public class WorkMasterProfile: Profile
    {
        public WorkMasterProfile()
        {
            CreateMap<WorkMasterEntity, GetWorkMasterResponse>();
            CreateMap<WorkMasterCommand, WorkMasterEntity>();
        }
    }
}
