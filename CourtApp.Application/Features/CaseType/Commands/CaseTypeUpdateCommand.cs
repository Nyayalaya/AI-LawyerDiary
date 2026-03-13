using CourtApp.Application.Common;
using CourtApp.Application.Features.CaseCategory.Services;
using CourtApp.Application.Features.CaseType.Services;
using CourtApp.Application.Interfaces.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CourtApp.Application.Features.Typeofcasess.Commands
{
    public class CaseTypeUpdateCommand : IRequest<Result<string>>
    {
        public Guid Id { get; set; }
        public Guid NatureId { get; set; }
        public string Name_En { get; set; }
        public string Name_Hn { get; set; }
        public string Abbreviation { get; set; }
        public Guid CourtTypeId { get; set; }
    }
}
