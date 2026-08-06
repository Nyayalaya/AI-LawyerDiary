using AutoMapper;
using CourtApp.Application.Features.DynamicProperty.Commands;
using CourtApp.Application.Features.DynamicProperty.Dtos;
using CourtApp.Domain.Entities.Masters;

namespace CourtApp.Application.Mappings;

public class DynamicPropertyProfile : Profile
{
    public DynamicPropertyProfile()
    {
        CreateMap<DynamicPropertyEntity, DynamicPropertyDto>();
        CreateMap<CreateDynamicPropertyCommand, DynamicPropertyEntity>();
        CreateMap<UpdateDynamicPropertyCommand, DynamicPropertyEntity>();
    }
}
