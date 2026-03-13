using AutoMapper;
using CourtApp.Application.DTOs.FormBuilder;
using CourtApp.Application.Features.FormBuilder;
using CourtApp.Domain.Entities.FormBuilder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourtApp.Application.Mappings
{
    public class CaseDraftingProfile:Profile
    {
        public CaseDraftingProfile()
        {
            CreateMap<CreateCaseDraftingDetailCommand, DraftingDetailEntity>();
            CreateMap<TemplateFields, FormFieldValueEntity>();
            CreateMap<FormFieldValueEntity, FormFieldDetailValue>();
           
        }
    }
}
