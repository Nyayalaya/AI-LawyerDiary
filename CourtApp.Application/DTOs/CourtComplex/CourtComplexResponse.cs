using CourtApp.Domain.Entities.Common;
using System;
using System.Collections.Generic;

namespace CourtApp.Application.DTOs.CourtComplex
{
    public class CourtComplexResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string StateName { get; set; }
        public string DistrictName { get; set; }
        public string CDistrictName { get; set; }
        public List<LangEntity> Languages { get; set; }
    }
}
