using AutoMapper;
using CourtApp.Application.Features.Publications.Command;
using CourtApp.Application.Features.Publications.Queries;
using CourtApp.Domain.Entities.LawyerDiary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourtApp.Application.Mappings
{
    public class PublicationProfile:Profile
    {
        public PublicationProfile()
        {
            CreateMap<CreatePublicationCommand, PublisherEntity>().ReverseMap();
            CreateMap<UpdatePublicationCommand, PublisherEntity>().ReverseMap();
            CreateMap<DeletePublicationCommand, PublisherEntity>().ReverseMap();
            CreateMap<GetPublicationByIdResponse, PublisherEntity>().ReverseMap();
            CreateMap<GetAllPublisherCachedResponse, PublisherEntity>().ReverseMap();
        }
    }
}
