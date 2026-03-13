using AutoMapper;
using CourtApp.Application.DTOs.DOTypes;
using CourtApp.Application.Features.CaseKinds.Query;
using CourtApp.Application.Features.DOType;
using CourtApp.Web.Areas.LawyerDiary.Models;

namespace CourtApp.Web.Areas.LawyerDiary.Mappings
{
    public class DOTypeProfileMapping:Profile
    {
        public DOTypeProfileMapping()
        {
            CreateMap<DOTypeViewModel,CreateDOTypeCommand>();
            CreateMap<DOTypeViewModel,UpdateDOTypeCommand>();
            CreateMap<DOTypeResponse, DOTypeViewModel>();
            CreateMap<DOTypeByIdResponse, DOTypeViewModel>();
        }
    }
}
