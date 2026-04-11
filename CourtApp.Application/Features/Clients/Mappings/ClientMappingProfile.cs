using AutoMapper;
using CourtApp.Application.Features.Clients.DTOs;
using CourtApp.Domain.Entities.LawyerDiary;

namespace CourtApp.Application.Features.Clients.Mappings
{
    /// <summary>
    /// AutoMapper profile for Client entity to DTO mappings
    /// </summary>
    public sealed class ClientMappingProfile : AutoMapper.Profile
    {
        public ClientMappingProfile()
        {
            // Create mappings
            CreateMap<ClientEntity, ClientResponseDto>()
                .ReverseMap();

            CreateMap<ClientEntity, ClientListDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id.ToString()))
                .ReverseMap();

            CreateMap<ClientCreateDto, ClientEntity>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());

            CreateMap<ClientUpdateDto, ClientEntity>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());
        }
    }
}
