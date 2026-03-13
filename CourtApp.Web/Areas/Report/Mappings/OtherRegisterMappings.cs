using AutoMapper;
using CourtApp.Application.DTOs.Registers;
using CourtApp.Web.Areas.Report.Models;

namespace CourtApp.Web.Areas.Report.Mappings
{
    public class RegisterMappings:Profile
    {
        public RegisterMappings()
        {
            CreateMap<OtherRegisterResponse, OtherRegisterViewModel>();
            CreateMap<DisposalRegisterResponse, DisposalRegisterViewModel>();
        }
    }
}
