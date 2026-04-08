using CourtApp.Domain.Enums;
using System;

namespace CourtApp.Application.Features.Auth.Dto
{
    public class RegisterRequest
    {
        public RegisterType UserType { get; set; }
        public string FirstName { get; set; }
        public string? MiddleName { get; set; }
        public string LastName { get; set; }
        public string? CompanyName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string EnrollmentNumber { get; set; } 
        public string RegistrationNumber { get; set; } 
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string Origin { get; set; }
    }
}