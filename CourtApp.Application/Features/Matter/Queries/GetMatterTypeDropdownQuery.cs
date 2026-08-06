using CourtApp.Application.Common;
using CourtApp.Application.DTOs.DropdownDtos;
using MediatR;
using System.Collections.Generic;

namespace CourtApp.Application.Features.Matter.Queries;

public sealed record GetMatterTypeDropdownQuery
    : IRequest<Result<List<DdlGuidStringDto>>>;