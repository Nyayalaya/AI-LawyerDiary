using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc;
using System;

namespace CourtApp.Web.Areas.Client.Model
{
    public class ClientDetailViewModel
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string FatherName { get; set; }
        public string Dob { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public string OfficeEmail { get; set; }
        public string Phone { get; set; }
        public bool IsRural { get; set; }
        public string Landmark { get; set; }
        public string Address { get; set; }      
    }
}
