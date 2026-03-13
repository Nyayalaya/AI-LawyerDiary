using AutoMapper;
using CourtApp.Application.DTOs.FormBuilder;
using CourtApp.Web.Areas.Admin.Models;

namespace CourtApp.Web.Areas.Admin.Mappings
{
    public class TemplateBuilderProfile:Profile
    {
        public TemplateBuilderProfile()
        {
            CreateMap<GetTemplateInfoDtoResponse, TemplateDlViewModel>();
            CreateMap<GetTemplateInfoByIdDto, TemplateViewModel>();
        }
    }
}
