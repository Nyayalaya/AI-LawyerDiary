using AutoMapper;
using CourtApp.Application.Features.ProceedingHead;
using CourtApp.Application.Features.WorkMaster;
using CourtApp.Web.Areas.LawyerDiary.Models;

namespace CourtApp.Web.Areas.LawyerDiary.Mappings
{
    public class WorkMasterProfile:Profile
    {
        public WorkMasterProfile()
        {
            CreateMap<GetWorkMasterResponse, WorkMasterViewModel>().ReverseMap();
            CreateMap<WorkMasterCommand, WorkMasterViewModel>().ReverseMap();
        }
    }
}
