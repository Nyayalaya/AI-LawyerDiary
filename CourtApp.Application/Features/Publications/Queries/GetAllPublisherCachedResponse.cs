using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourtApp.Application.Features.Publications.Queries
{
    public class GetAllPublisherCachedResponse
    {
        public Guid Id { get; set; }
        public string PublicationName { get; set; }
        public string PropriatorName { get; set; }
    }
}
