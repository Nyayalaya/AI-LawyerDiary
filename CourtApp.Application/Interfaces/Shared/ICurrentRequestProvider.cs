using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourtApp.Application.Interfaces.Shared
{
    public interface ICurrentRequestProvider
    {
        string GetOrigin();
        string GetIpAddress();
    }
}
