using System;
using CourtApp.Application.Common;
using CourtApp.Application.Features.Clients.DTOs;
using MediatR;

namespace CourtApp.Application.Features.Clients.Commands.CreateClient
{
    public sealed class CreateClientCommand : IRequest<Result<Guid>>
    {
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
        public string UserId { get; set; }
    }
}
