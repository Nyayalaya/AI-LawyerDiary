using CourtApp.Infrastructure.Identity.Models;
using CourtApp.Web.Areas.Admin.Models;
using AutoMapper;

namespace CourtApp.Web.Areas.Admin.Mappings
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<ApplicationUser, UserViewModel>().ReverseMap();
        }
    }
}