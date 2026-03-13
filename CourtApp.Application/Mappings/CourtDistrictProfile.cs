using AutoMapper;
using CourtApp.Application.DTOs.CourtDistrict;
using CourtApp.Application.Features.BookTypes.Command;
using CourtApp.Application.Features.CourtDistrict;
using CourtApp.Domain.Entities.LawyerDiary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourtApp.Application.Mappings
{
    public class CourtDistrictProfile : Profile
    {
        public CourtDistrictProfile()
        {
            CreateMap<CourtDistrictEntity, CourtDistrictByIdReponse>();
            CreateMap<CourtDistrictEntity, CourtDistrictReponse>();
            CreateMap<CreateCourtDistrictCommand, CourtDistrictEntity>();
            CreateMap<UpdateCourtDistrictCommand, CourtDistrictEntity>();
            
        }
    }
}
