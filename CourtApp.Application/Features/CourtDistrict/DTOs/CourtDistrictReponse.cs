using CourtApp.Domain.Entities.Common;
using System;
using System.Collections.Generic;

namespace CourtApp.Application.Features.CourtDistrict.DTOs
{
    public class CourtDistrictReponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int StateId { get; set; }
        public string StateName { get; set; }
        public List<LangEntity> Languages { get; set; }
    }
}
