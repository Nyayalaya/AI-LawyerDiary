using CourtApp.Application.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourtApp.Application.Features.Auth.Commands
{
    public class ApproveUserCommand : IRequest<Result<string>>
    {
        public string UserId { get; set; }
    }
}
