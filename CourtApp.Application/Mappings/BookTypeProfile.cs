using AutoMapper;
using CourtApp.Application.Features.BookTypes.Command;
using CourtApp.Application.Features.BookTypes.Query.GetAllCached;
using CourtApp.Application.Features.BookTypes.Query.GetById;
using CourtApp.Application.Features.Typeofcasess.Query;
using CourtApp.Domain.Entities.LawyerDiary;

namespace CourtApp.Application.Mappings
{
    public class BookTypeProfile : Profile
    {
        public BookTypeProfile()
        {

            CreateMap<CreateBookTypeCommand, BookTypeEntity>();

            CreateMap<GetBookTypeByIdResponse, BookTypeEntity>().ReverseMap();
            CreateMap<BookTypeEntity, GetAllBookTypeCachedResponse>();
                
        }
    }
}
