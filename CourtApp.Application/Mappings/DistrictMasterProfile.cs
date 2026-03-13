using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CourtApp.Application.Features.Queries.Districts;
using CourtApp.Entities.Common;

namespace CourtApp.Application.Mappings
{
    public class DistrictMasterProfile:Profile
    {
        public DistrictMasterProfile()
        {
             CreateMap<GetDistrictResponse, DistrictEntity>().ReverseMap();
        }
    }
}