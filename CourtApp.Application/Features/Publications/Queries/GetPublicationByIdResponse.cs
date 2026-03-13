using System;

namespace CourtApp.Application.Features.Publications.Queries
{
    public class GetPublicationByIdResponse
    {
        public Guid Id { get; set; }
        public string PublicationName { get; set; }
        public string PropriatorName { get; set; }
    }
}
