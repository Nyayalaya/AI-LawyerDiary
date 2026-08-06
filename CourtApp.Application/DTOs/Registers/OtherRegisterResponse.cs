using CourtApp.Application.Features.CaseDetails.Dtos;
namespace CourtApp.Application.DTOs.Registers
{
    public class OtherRegisterResponse : CaseBasicInfoDto
    {
        public string WorkType { get; set; }
        public string WorkDone { get; set; }
        public string WorkDate { get; set; }
       
    }
}
