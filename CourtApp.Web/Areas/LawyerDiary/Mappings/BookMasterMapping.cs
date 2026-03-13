using AutoMapper;
using CourtApp.Application.Features.BookMasters.Command;
using CourtApp.Application.Features.BookMasters.Query;
using CourtApp.Web.Areas.LawyerDiary.Models;

namespace CourtApp.Web.Areas.LawyerDiary.Mappings
{
    public class BookMasterMapping:Profile
    {
        public BookMasterMapping()
        {
            CreateMap<GetAllBookMasterCacheResponse, BookMasterViewModel>().ReverseMap();
            CreateMap<GetAllBookMasterResponse, BookMasterViewModel>().ReverseMap();
            CreateMap<GetBookMasterByIdResponse, BookMasterViewModel>().ReverseMap();
            CreateMap<CreateBookMasterCommand, BookMasterViewModel>().ReverseMap();
            CreateMap<UpdateBookMasterCommand, BookMasterViewModel>().ReverseMap();
            CreateMap<DeleteBookMasterCommand, BookMasterViewModel>().ReverseMap();
           
        }

    }
}
