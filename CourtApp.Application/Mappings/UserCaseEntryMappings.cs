using AutoMapper;
using CourtApp.Application.Features.Commands.Cases;
using CourtApp.Domain.Entities.LawyerDiary;

namespace CourtApp.Application.Mappings
{
    internal class UserCaseEntryMappings : Profile
    {
        public UserCaseEntryMappings()
        {
           
            CreateMap<CommandCreateCaseEntry, CaseEntity>().ReverseMap();
            CreateMap<CommandUpdateCaseEntry, CaseEntity>().ReverseMap();
            CreateMap<CommandDeleteCaseEntry, CaseEntity>().ReverseMap();
            
        }
    
    }
}
