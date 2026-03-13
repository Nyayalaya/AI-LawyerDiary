using System;

namespace CourtApp.Web.Areas.Client.Model
{
    public class CorporateViewModel
    {
        public Guid Id { get; set; }
        public string FirmName { get; set; }
        public string FirmEmail { get; set; }
        public string FirmContact { get; set; }
        public string FirmType { get; set; }
        public string Address { get; set; }
        public string RegNo { get; set; }
        public string Owner { get; set; }
        public string OfficeNo { get; set; }
    }
}
