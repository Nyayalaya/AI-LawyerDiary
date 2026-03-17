using CourtApp.Application.Common;
using MediatR;

namespace CourtApp.Application.Features.Clients.Commands
{
    public class ClientCreateCommand : IRequest<Result<string>>
    {
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
        public string UserId { get; set; }
    }
}