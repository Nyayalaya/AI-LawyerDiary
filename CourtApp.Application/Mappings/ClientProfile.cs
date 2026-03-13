using AutoMapper;
using CourtApp.Application.DTOs.Client;
using CourtApp.Application.Features.Clients.Commands;
using CourtApp.Application.Features.Clients.Queries.GetAllCached;
using CourtApp.Application.Features.Clients.Queries.GetById;
using CourtApp.Domain.Entities.Account;
using CourtApp.Domain.Entities.LawyerDiary;

namespace CourtApp.Application.Mappings
{
    public class ClientProfile : Profile
    {
        public ClientProfile()
        {
            CreateMap<CreateClientCommand, ClientEntity>();
            CreateMap<ClientEntity, GetClientByIdResponse>();
            CreateMap<GetAllClientCachedResponse, ClientEntity>().ReverseMap();
            CreateMap<CaseFeeEntity, ClientFeeDto>();
            CreateMap<ClientEntity, GetClientInfoDto>();
        }
    }
}
