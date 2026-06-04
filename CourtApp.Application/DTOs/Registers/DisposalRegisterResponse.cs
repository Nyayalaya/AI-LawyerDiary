using CourtApp.Application.Features.CaseDetails.Dtos;
namespace CourtApp.Application.DTOs.Registers
{
    public class DisposalRegisterResponse:CaseBasicInfoDto
    {       
        public string Reason { get; set; }
        public string DisposalDate { get; set; }
        
    }
}
