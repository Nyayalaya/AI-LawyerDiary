using AutoMapper;
using CourtApp.Application.DTOs.CourtMaster;
using CourtApp.Application.Features.CourtMasters.Command;
using CourtApp.Domain.Entities.LawyerDiary;
using CourtApp.Web.Areas.LawyerDiary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CourtBench = CourtApp.Web.Areas.LawyerDiary.Models.CourtBench;

namespace CourtApp.Web.Areas.LawyerDiary.Mappings
{
    public class CourtMasterMapping : Profile
    {
        public CourtMasterMapping()
        {
            CreateMap<CourtMasterViewModel, CreateCourtMasterCommand>();
               

            CreateMap<GetCourtMasterDataAllResponse, CourtMasterViewModel>().ReverseMap();
            CreateMap<CourtBenchEntity, CourtBench>()
                .ForPath(d => d.CourtBench_En, opt => opt.MapFrom(src => src.CourtBench_En))
                .ForPath(d => d.Address, opt => opt.MapFrom(src => src.Address));

            CreateMap<UpdateCourtMasterCommand, CourtMasterViewModel>().ReverseMap();
            CreateMap<GetCourtMasterDataByIdResponse, CourtMasterViewModel>()
                .ForPath(d => d.StateCode, opt => opt.MapFrom(src => src.StateId))
                /*.ForPath(d => d.CourtName, opt => opt.MapFrom(src => src.Name_En))*/;
            CreateMap<DeleteCourtMasterCommand, CourtMasterViewModel>().ReverseMap();
            CreateMap<CourtBench, Application.DTOs.CourtMaster.CourtBenchResponse>().ReverseMap();
            CreateMap<GetCourtMasterDataAllResponse, CourtMasterListModel>().ReverseMap();

        }
    }
}
