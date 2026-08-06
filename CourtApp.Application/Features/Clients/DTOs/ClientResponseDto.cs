using System;

namespace CourtApp.Application.Features.Clients.DTOs
{
    public class ClientResponseDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public string OfficeEmail { get; set; }
        public string Phone { get; set; }
        public string ReferralBy { get; set; }
        public string RegNo { get; set; }
        public string Proprietor { get; set; }
        public string ClientType { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
