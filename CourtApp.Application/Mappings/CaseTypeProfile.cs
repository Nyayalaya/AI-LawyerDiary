using AutoMapper;
using AutoMapper.Configuration.Conventions;
using CourtApp.Application.Features.Typeofcasess.Commands;
using CourtApp.Application.Features.Typeofcasess.Query;
using CourtApp.Domain.Entities.LawyerDiary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourtApp.Application.Mappings
{
    public class TypeofcasesProfile : Profile
    {
        public TypeofcasesProfile()
        {
            CreateMap<TypeOfCasesCacheQueryResponse, TypeOfCasesEntity>().ReverseMap();

            CreateMap<TypeOfCasesEntity, TypeOfCasesQueryByIdResponse>()
                .ForMember(d => d.NatureId, opt => opt.MapFrom(src => src.Nature.Id))
                .ForMember(d => d.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(d => d.Name_En, opt => opt.MapFrom(src => src.Name_En));

            CreateMap<CaseTypeCreateCommand, TypeOfCasesEntity>()
                .ReverseMap();
            CreateMap<CaseTypeUpdateCommand, TypeOfCasesEntity>().ReverseMap();
            CreateMap<CaseTypeDeleteCommand, TypeOfCasesEntity>().ReverseMap();
        }
    }
}
