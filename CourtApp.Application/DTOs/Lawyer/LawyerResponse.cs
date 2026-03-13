using System;

namespace CourtApp.Application.DTOs.Lawyer
{
    public class LawyerResponse
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
        public string ProfileImgPath { get; set; }
    }
}
