using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourtApp.Domain.Enums
{
    public enum AddressType
    {
        Permanent = 1,        // Home / Legal residence
        Current = 2,          // Current living address
        Office = 3,           // Office address
        Chamber = 4,          // Court chamber (very important for lawyers)
        Correspondence = 5,   // Mailing / communication
        Other = 6
    }
}
