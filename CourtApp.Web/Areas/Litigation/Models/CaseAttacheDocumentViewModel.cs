using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;

namespace CourtApp.Web.Areas.Litigation.Models
{
    public class CaseAttacheDocumentViewModel
    {
        public Guid CaseId { get; set; }
        public Guid DOTypeId { get; set; }
        public Guid DOId { get; set; }
        public string Title { get; set; }
        public string Court { get; set; }
        public string CaseNoYear { get; set; }
        public string Reference { get; set; }
        public List<CaseDoc> Docs { get; set; }
        public SelectList DocTypes { get; set; }
        public List<AttachmentModel> Documents { get; set; }
        public string Where { get; set; }
    }

    public class AttachmentModel
    {
        public int TypeId { get; set; }
        public Guid DocId { get; set; }
        public DateTime DocDate { get; set; }
        public IFormFile Document { get; set; }
    }
}
