using AutoMapper;
using CourtApp.Application.Features.BookMasters.Command;
using CourtApp.Application.Features.BookMasters.Query;
using CourtApp.Domain.Entities.LawyerDiary;

namespace CourtApp.Application.Mappings
{
    public class BookMasterProfile : Profile
    {
        public BookMasterProfile()
        {
            CreateMap<GetBookMasterByIdResponse, LDBookEntity>().ReverseMap();
            
            CreateMap<CreateBookMasterCommand, LDBookEntity>().ReverseMap();
            CreateMap<UpdateBookMasterCommand, LDBookEntity>().ReverseMap();
            CreateMap<DeleteBookMasterCommand, LDBookEntity>().ReverseMap();

            CreateMap<LDBookEntity, GetAllBookMasterCacheResponse>()
                .ForMember(d => d.BookType, opt => opt.MapFrom(src => src.BookType.Name_En));
                /*.ForMember(d => d.BookName, opt => opt.MapFrom(src => src.N))*/;
        }

    }
}
