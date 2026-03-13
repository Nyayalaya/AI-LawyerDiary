using System;

namespace CourtApp.Application.DTOs.Client
{
    public class GetClientInfoDto
    {
        public Guid Id { get; set; }
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
    }
}
