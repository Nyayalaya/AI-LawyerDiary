using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourtApp.Infrastructure.AI.Models.DTOs
{
    public sealed record ChatAttachment
    {
        public string FileName { get; set; } = string.Empty;

        public string ContentType { get; set; } = string.Empty;

        public string Url { get; set; } = string.Empty;

        public long Size { get; set; }
    }
}
