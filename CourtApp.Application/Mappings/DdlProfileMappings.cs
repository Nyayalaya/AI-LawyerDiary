using AutoMapper;
using CourtApp.Application.DTOs.DropDowns;
using CourtApp.Application.Features.Clients.Commands;
using CourtApp.Domain.Entities.LawyerDiary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourtApp.Application.Mappings
{
    public class DdlProfileMappings : Profile
    {
        public DdlProfileMappings()
        {
            CreateMap<LawyerMasterEntity,DdlGuidStringDto>()
                .ForPath(d =>d.Name , s => s.MapFrom(src => ((src.FirstName + " " + src.MiddleName + " " + src.LastName))));
        }
    }
}
