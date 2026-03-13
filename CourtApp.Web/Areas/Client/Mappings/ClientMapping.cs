using AutoMapper;
using CourtApp.Application.DTOs.Client;
using CourtApp.Application.Features.Clients.Commands;
using CourtApp.Application.Features.Clients.Queries.GetAllCached;
using CourtApp.Application.Features.Clients.Queries.GetById;
using CourtApp.Web.Areas.Client.Model;
using CourtApp.Web.Areas.Litigation.Models;

namespace CourtApp.Web.Areas.Client.Mappings
{
    public class ClientMapping : Profile
    {
        public ClientMapping()
        {
            CreateMap<GetAllClientCachedResponse, GClientViewModel>();
            CreateMap<ClientViewModel, CreateClientCommand>();
            CreateMap<ClientViewModel, UpdateClientCommand>();
            CreateMap<GetClientByIdResponse, ClientViewModel>();
            CreateMap<CreateClientCommand, ClientViewModel>();
            CreateMap<UpdateClientCommand, ClientViewModel>();
            CreateMap<GetClientInfoDto, DropDownSViewModel>()
                 .ForPath(d => d.Id, sr => sr.MapFrom(s => s.ReferalBy.Trim().ToUpper()))
                 .ForPath(d => d.Name, sr => sr.MapFrom(s => s.ReferalBy.Trim().ToUpper()));
            CreateMap<GetClientInfoDto, DropDownGViewModel>();

        }
    }
}
