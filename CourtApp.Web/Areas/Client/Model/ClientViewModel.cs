using Microsoft.AspNetCore.Mvc;
using System;

namespace CourtApp.Web.Areas.Client.Model
{
    public class ClientViewModel
    {
        public Guid Id { get; set; }
        [TempData]
        public string StatusMessage { get; set; }
        public Guid CaseId { get; set; }
        public string Appearence { get; set; }

        #region Client Properties
        public string Name { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public string OfficeEmail { get; set; }
        public string Phone { get; set; }
        public string ReferalBy { get; set; }
        public string RegNo { get; set; }
        public string Properiter { get; set; }
        public string ClientType { get; set; }

        #endregion

    }
}
