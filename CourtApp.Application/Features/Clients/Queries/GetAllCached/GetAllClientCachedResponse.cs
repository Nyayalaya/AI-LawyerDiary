using System;

namespace CourtApp.Application.Features.Clients.Queries.GetAllCached
{
    public class GetAllClientCachedResponse
    {
        public Guid Id { get; set; }
        public string ClientType { get; set; }
        public string OffEmail { get; set; }
        public string Councel { get; set; }
        public string Appearence { get; set; }
        public string ReferalBy { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }

    }
}