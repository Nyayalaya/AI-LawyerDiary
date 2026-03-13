using System;
using CourtApp.Application.Common;
using MediatR;

namespace CourtApp.Application.Features.Typeofcasess.Commands
{
   public class CaseTypeDeleteCommand : IRequest<Result<string>>
    {
        public Guid Id { get; set; }              
    }

    
}
