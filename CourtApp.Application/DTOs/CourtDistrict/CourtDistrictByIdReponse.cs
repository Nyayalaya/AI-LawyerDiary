using CourtApp.Domain.Entities.Common;
using System;
using System.Collections.Generic;

namespace CourtApp.Application.DTOs.CourtDistrict
{
    public class CourtDistrictByIdReponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public int StateId { get; set; }
        public List<LangEntity> Languages { get; set; }
    }
}
