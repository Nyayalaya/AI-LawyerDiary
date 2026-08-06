using System;
using CourtApp.Application.Features.CaseDetails.Dtos;
namespace CourtApp.Application.DTOs.Registers
{
    public class InstitutionResponse : CaseBasicInfoDto
    {
        public bool IsCaseAssigned { get; set; }
        public string LawyerId { get; set; }
    }
}
