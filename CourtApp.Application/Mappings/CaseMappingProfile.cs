using AutoMapper;
using CourtApp.Application.Features.CaseDetails.Dtos;
using CourtApp.Application.Features.CaseDetails.Extensions;
using CourtApp.Domain.Entities.CaseDetails;
using System.Linq;
namespace CourtApp.Application.Mappings
{
    public class CaseMappingProfile : Profile
    {
        public CaseMappingProfile()
        {
            CreateMap<CaseEntity, CaseBasicInfoDto>()
               .MapCaseBasicInfo();

            CreateMap<CaseEntity, CaseDataListDto>()
                .IncludeBase<CaseEntity, CaseBasicInfoDto>()

                .ForMember(d => d.AssignedLawyerId,
                    o => o.MapFrom(s =>
                        s.CaseAssignedEntities
                            .OrderByDescending(x => x.CreatedOn)
                            .Select(x => x.LawyerId)
                            .FirstOrDefault()));

            CreateMap<CaseEntity, CaseHistoryDto>()
                .IncludeBase<CaseEntity, CaseBasicInfoDto>();

        }
    }
}
