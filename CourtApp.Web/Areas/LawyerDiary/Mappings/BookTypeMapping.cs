using AutoMapper;
using CourtApp.Application.Features.BookTypes.Command;
using CourtApp.Application.Features.BookTypes.Query.GetAllCached;
using CourtApp.Application.Features.BookTypes.Query.GetById;
using CourtApp.Web.Areas.LawyerDiary.Models;

namespace CourtApp.Web.Areas.LawyerDiary.Mappings
{
    public class BookTypeMapping:Profile
    {
        public BookTypeMapping()
        {
            CreateMap<GetAllBookTypeCachedResponse, BookTypeViewModel>().ReverseMap();
            CreateMap<GetBookTypeByIdResponse, BookTypeViewModel>().ReverseMap();
            CreateMap<CreateBookTypeCommand, BookTypeViewModel>().ReverseMap();
            CreateMap<UpdateCreateBookTypeCommand, BookTypeViewModel>().ReverseMap();
        }
    }
}
