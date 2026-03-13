using AutoMapper;
using CourtApp.Application.DTOs.CaseWorking;
using CourtApp.Application.Features.Case;
using CourtApp.Application.Features.CaseWork;
using CourtApp.Web.Areas.Litigation.Models;

namespace CourtApp.Web.Areas.Litigation.Mappings
{
    public class CaseWorkingProfileMapping:Profile
    {
        public CaseWorkingProfileMapping()
        {
            CreateMap<AssignedWorkToCaseResponse, CaseWorkingViewModel>();            
            CreateMap<CaseWorkingViewModel, CreateCaseWorkCommand>();
        }
    }
}
