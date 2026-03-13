using CourtApp.Domain.Entities.LawyerDiary;
using System;
using System.Collections.Generic;

namespace CourtApp.Application.DTOs.CourtMaster
{
    public class GetCourtMasterDataByIdResponse
    {
        public Guid Id { get; set; }
        public int StateId { get; set; }
        public Guid CourtTypeId { get; set; }
        public int DistrictCode { get; set; }
        public Guid CourtDistrictId { get; set; }
        public Guid CourtComplexId { get; set; }
        public string Name_En { get; set; }
        public string Address { get; set; }
        public string Name_Hn { get; set; }
        public string Abbreviation { get; set; }
        public bool IsHighCourt { get; set; }
        public virtual IList<CourtBenchEntity> CourtBenches { get; set; }

    }
}
