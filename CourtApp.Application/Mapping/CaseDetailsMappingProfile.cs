using AutoMapper;
using CourtApp.Application.Features.CaseDetails.Dtos;
using CourtApp.Domain.Entities; // Adjust based on your entity structure

namespace CourtApp.Application.Mapping
{
    /// <summary>
    /// AutoMapper profile for Case-related DTOs
    /// Centralizes all case DTO mappings to maintain consistency
    /// </summary>
    public class CaseDetailsMappingProfile : Profile
    {
        public CaseDetailsMappingProfile()
        {
            // CaseBasicInfoDto mappings
            CreateMap<CaseBasicInfoDto, CaseDataListDto>()
                .ReverseMap();

            // CaseBasicInfoDto to CaseResponseDto
            // Note: CaseResponseDto extends CaseBasicInfoDto, so mapping is simple
            CreateMap<CaseBasicInfoDto, CaseResponseDto>()
                .ReverseMap();

            // CaseBasicInfoDto to CaseMinimalResponseDto
            CreateMap<CaseBasicInfoDto, CaseMinimalResponseDto>()
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.CaseTitle))
                .ForMember(dest => dest.CourtName, opt => opt.MapFrom(src => src.Court))
                .ForMember(dest => dest.CaseCategory, opt => opt.MapFrom(src => src.CaseType))
                .ReverseMap();

            // Note: Add actual entity-to-DTO mappings when domain entities are available
            // Example:
            // CreateMap<UserCase, CaseBasicInfoDto>()
            //     .ForMember(dest => dest.CaseNumberYear, opt => opt.MapFrom(src => $"{src.CaseNo}-{src.CaseYear}"))
            //     .ForMember(dest => dest.InstitutionDate, opt => opt.MapFrom(src => src.InstitutionDate.ToString("dd/MM/yyyy")))
            //     .ReverseMap();
        }
    }
}