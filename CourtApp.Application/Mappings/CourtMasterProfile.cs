using AutoMapper;
using CourtApp.Application.DTOs.CourtMaster;
using CourtApp.Application.Features.CourtMasters.Command;
using CourtApp.Domain.Entities.LawyerDiary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourtApp.Application.Mappings
{
    public class CourtMasterProfile:Profile
    {
        public CourtMasterProfile()
        {
            CreateMap<CreateCourtMasterCommand, CourtMasterEntity>()
                .ForPath(d => d.StateId, opt => opt.MapFrom(src => src.StateCode))
                //.ForPath(d => d.DistrictId, opt => opt.MapFrom(src => src.DistrictCode))
                .ForPath(d => d.Name_En, opt => opt.MapFrom(src => src.CourtName))
                //.ForPath(d => d.CourtTypeId, opt => opt.MapFrom(src => src.CourtTypeId))
                //.ForPath(d => d.CourtDistrictId, opt => opt.MapFrom(src => src.CourtDistrictId))
                //.ForPath(d => d.CourtComplexId, opt => opt.MapFrom(src => src.CourtComplexId))
                ;

            CreateMap<UpdateCourtMasterCommand, CourtMasterEntity>().ReverseMap();
            CreateMap<DeleteCourtMasterCommand, CourtMasterEntity>().ReverseMap();
            CreateMap<CourtMasterEntity, GetCourtMasterDataByIdResponse>()
                .ForPath(d => d.CourtTypeId, opt => opt.MapFrom(src => src.CourtTypeId))
                .ForPath(d => d.StateId, opt => opt.MapFrom(src => src.StateId))
                //.ForPath(d => d.DistrictCode, opt => opt.MapFrom(src => src.DistrictId))
                .ForPath(d => d.Name_En, opt => opt.MapFrom(src => src.Name_En));

            CreateMap<GetCourtMasterDataAllResponse, CourtMasterEntity>().ReverseMap();
            CreateMap<CourtBenchResponse, CourtBenchEntity>().ReverseMap();
            
        }
    }
}
