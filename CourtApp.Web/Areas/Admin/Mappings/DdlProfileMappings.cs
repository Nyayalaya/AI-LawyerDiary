using AutoMapper;
using CourtApp.Application.DTOs.CaseDetails;
using CourtApp.Application.DTOs.DropDowns;
using CourtApp.Web.Areas.Litigation.Models;

namespace CourtApp.Web.Areas.Admin.Mappings
{
    public class DdlProfileMappings : Profile
    {
        public DdlProfileMappings()
        {
            CreateMap<DdlGuidStringDto, DropDownGViewModel>();
            CreateMap<GetCaseInfoDto, DropDownGViewModel>()
                .ForPath(d => d.Name, s => s.MapFrom(src => src.CaseDetail));
        }
    }
}
