using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourtApp.Worker.Queues
{
    public static class QueueNames
    {
        public const string Documents = "documents";
        public const string Extraction = "extraction";
        public const string Chunking = "chunking";
        public const string Embedding = "embedding";
        public const string Citation = "citation";
    }
}
