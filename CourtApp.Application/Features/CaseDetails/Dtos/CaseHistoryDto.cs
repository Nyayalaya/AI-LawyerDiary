using System;
using System.Collections.Generic;

namespace CourtApp.Application.Features.CaseDetails.Dtos
{
    public class CaseHistoryDto:CaseBasicInfoDto
    {   
        public List<CaseActivityDto> History { get; set; }
        public List<CaseDocumentDto> Docs { get; set; }
    }
}
