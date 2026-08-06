using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourtApp.Infrastructure.AI.Models.Requests
{
    public sealed class AudioRequest
    {
        public string FilePath { get; set; } = string.Empty;

        public string? Model { get; set; }
    }
}
