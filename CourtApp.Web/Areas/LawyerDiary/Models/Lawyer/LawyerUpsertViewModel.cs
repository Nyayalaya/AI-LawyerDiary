using Microsoft.AspNetCore.Mvc.Rendering;
using System;

namespace CourtApp.Web.Areas.LawyerDiary.Models.Lawyer
{
    public class LawyerUpsertViewModel
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public DateTime Dob { get; set; }
        public string LastName { get; set; }
        public string EnrollNumber { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string Gender { get; set; }
        public SelectList Genders { get; set; }
        public string Religion { get; set; }
        public SelectList Relegions { get; set; }
        public string Caste { get; set; }
        public string RelatedPerson { get; set; }
        public string ProfileImgPath { get; set; }
    }
}
