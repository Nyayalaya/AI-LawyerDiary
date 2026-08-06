namespace CourtApp.Application.Features.Clients.DTOs
{
    public class ClientListDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public string ClientType { get; set; }
        public string ReferalBy { get; set; }
        public bool IsActive { get; set; }
    }
}
