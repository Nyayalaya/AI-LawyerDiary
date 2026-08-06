using AutoMapper;
using CourtApp.Application.Features.Matter.Commands;
using CourtApp.Application.Features.Matter.Dtos;
using CourtApp.Domain.Entities.Masters;

namespace CourtApp.Application.Mappings;

public class MatterProfile:Profile
{
    public MatterProfile()
    {
        CreateMap<MatterTypeEntity, MatterTypeDto>();

        CreateMap<CreateMatterTypeCommand, MatterTypeEntity>();

        CreateMap<UpdateMatterTypeCommand, MatterTypeEntity>();

        CreateMap<MatterCategoryEntity, MatterCategoryDto>()
            .ForMember(x => x.MatterTypeName,
                opt => opt.MapFrom(src => src.MatterType.Name));

       

        CreateMap<MatterSubCategoryEntity, MatterSubCategoryDto>()
            .ForMember(x => x.MatterCategoryName,
                opt => opt.MapFrom(src => src.MatterCategory.Name));

        
    }
}