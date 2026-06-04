using CourtApp.Application.Features.CaseDetails.Dtos;
namespace CourtApp.Application.DTOs.Registers
{
    public class CopyDisposalResponse: CaseBasicInfoDto
    {
        public string Reason { get; set; }
        public string AppliedOn { get; set; }
        public string ReceivedOn { get; set; }
    }
}
