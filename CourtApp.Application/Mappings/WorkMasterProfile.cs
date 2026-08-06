using AutoMapper;
using CourtApp.Application.Features.WorkMaster.Commands;
using CourtApp.Application.Features.WorkMaster.Dtos;
using CourtApp.Domain.Entities.Masters;

namespace CourtApp.Application.Mappings
{
    public class WorkMasterProfile: Profile
    {
        public WorkMasterProfile()
        {
            // Map WorkTypeEntity to response DTOs
            CreateMap<WorkTypeEntity, WorkMasterResponse>()
               ;

            CreateMap<WorkTypeEntity, WorkMasterResponse>();
            CreateMap<WorkTypeEntity, WorkMasterByIdResponse>();

            // Map Commands to Entity for Create/Update operations
            CreateMap<CreateWorkMasterCommand, WorkTypeEntity>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name_En))
                .ForMember(dest => dest.Code, opt => opt.MapFrom(src => src.Abbreviation));

            CreateMap<UpdateWorkMasterCommand, WorkTypeEntity>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name_En))
                .ForMember(dest => dest.Code, opt => opt.MapFrom(src => src.Abbreviation));
        }
    }
}
