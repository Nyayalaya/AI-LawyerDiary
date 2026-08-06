using CourtApp.Application.Common;
using AutoMapper;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using CourtApp.Application.Features.CaseStage.Services;

namespace CourtApp.Application.Features.CaseStages.Query
{
    public class CaseStageByIdQuery : IRequest<Result<CaseStageQueryByIdResponse>>
    {
        public Guid Id { get; set; }
    }

    
}
