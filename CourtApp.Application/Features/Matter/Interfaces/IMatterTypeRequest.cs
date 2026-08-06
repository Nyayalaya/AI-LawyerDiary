using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourtApp.Application.Features.Matter.Interfaces
{
    public interface IMatterTypeRequest
    {
        string Code { get; }
        string Name { get; }
        string? Description { get; }
        int DisplayOrder { get; }
    }
}
