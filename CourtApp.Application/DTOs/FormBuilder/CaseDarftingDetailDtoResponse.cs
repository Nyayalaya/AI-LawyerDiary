using System;
using System.Collections.Generic;
namespace CourtApp.Application.DTOs.FormBuilder
{
    public class CaseDarftingDetailDtoResponse
    {
        public Guid Id { get; set; }
        public string CaseTitle { get; set; }
        public string TemplateName { get; set; }
        public List<FieldsDetail> FieldsDetails { get; set; }
    }
    public class FieldsDetail
    {
        public string LabelName { get; set; }
        public string LableValue { get; set; }
    }
}
