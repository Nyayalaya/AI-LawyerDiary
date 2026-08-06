using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourtApp.Infrastructure.AI.Models.Requests
{
    public sealed class ImageRequest
    {
        public string Prompt { get; set; } = string.Empty;

        public string? Model { get; set; }

        public string Size { get; set; } = "1024x1024";
    }
}
