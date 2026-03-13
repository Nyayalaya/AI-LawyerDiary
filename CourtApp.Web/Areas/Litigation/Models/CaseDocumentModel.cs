using System;

namespace CourtApp.Web.Areas.Litigation.Models
{
    public class CaseDocumentModel
    {
        public int TypeId { get; set; }
        public Guid DocId { get; set; }
        public string DocPath { get; set; }
        public DateTime DocDate { get; set; }
    }
}
