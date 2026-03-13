using AutoMapper;
using CourtApp.Application.DTOs.WorkSub;
using CourtApp.Application.Features.ProceedingHead;
using CourtApp.Application.Features.WorkMasterSub;
using CourtApp.Web.Areas.LawyerDiary.Models;

namespace CourtApp.Web.Areas.LawyerDiary.Mappings
{
    public class WorkMasterSubProfile:Profile
    {
        public WorkMasterSubProfile()
        {
            CreateMap<WorkSubMasterResponse, WorkMasterSubViewModel>();
            CreateMap<WorkSubMasterByIdResponse, WorkMasterSubViewModel>();
            CreateMap<WorkMasterSubViewModel,CreateWorkSubMstCommand>();
            CreateMap<WorkMasterSubViewModel,UpdateWorkSubMstCommand>();
            CreateMap<WorkMasterSubViewModel,DeleteWorkSubMstCommand>();
            CreateMap<WorkSubMaster, WrkSubMaster>();
        }
    }
}
