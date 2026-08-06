using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourtApp.Infrastructure.AI.Configuration.Kernel
{
    public sealed class KernelOptions
    {
        public bool AutoInvokeFunctions { get; set; }
        public bool EnablePlanning { get; set; }
        public bool EnableMemory { get; set; }
        public bool EnableStreaming { get; set; }
        public int MaxFunctions { get; set; }
    }
}
