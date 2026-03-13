using AutoMapper;
using CourtApp.Application.DTOs.DOTypes;
using CourtApp.Application.Features.DOType;
using CourtApp.Domain.Entities.LawyerDiary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourtApp.Application.Mappings
{
    public class DOTypeMapping:Profile
    {
        public DOTypeMapping()
        {
            CreateMap<DOTypeEntity,DOTypeResponse>().ReverseMap();
            CreateMap<DOTypeEntity, DOTypeByIdResponse>().ReverseMap();
            CreateMap<CreateDOTypeCommand,DOTypeEntity>();
            CreateMap<UpdateDOTypeCommand,DOTypeEntity>();
        }
    }
}
