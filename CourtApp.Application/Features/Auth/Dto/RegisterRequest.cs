using CourtApp.Domain.Enums;
using System;

namespace CourtApp.Application.Features.Auth.Dto
{
    public class RegisterRequest
    {
        public RegisterType UserType { get; set; }
        public IndividualInfoDto? IndividualInfoDto { get; set; }
        public CompanyInfoDto? CompanyInfoDto { get; set; }
        public string Email { get; set; }
        public string Contact { get; set; }
        public string Password { get; set; }
        public string Origin { get; set; }
    }

    public record IndividualInfoDto
    {
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public Gender Gender { get; set; }
        public string EnrollmentNumber { get; set; }
    }

    public record CompanyInfoDto
    {
        public string CompanyName { get; set; }
        public string RegistrationNumber { get; set; }
        public DateTime? IncorporationDate { get; set; }
        public string GstNumber { get; set; }
        public string AutherizedPerson { get; set; }
    }
}