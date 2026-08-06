using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CourtApp.Application.Features.Queries.Districts;
using CourtApp.Domain.Entities.Masters;

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