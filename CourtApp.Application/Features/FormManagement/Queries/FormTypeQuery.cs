using CourtApp.Application.Common;
using CourtApp.Application.Features.FormManagement.DTOs;
using MediatR;
using System;
using System.Collections.Generic;

namespace CourtApp.Application.Features.FormManagement.Queries
{
    public class GetAllFormTypesQuery : IRequest<PaginatedResult<FormTypeResponseDto>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }

    public class GetFormTypeByIdQuery : IRequest<FormTypeResponseDto>
    {
        public Guid Id { get; set; }
    }

    public class GetFormTypeByCodeQuery : IRequest<FormTypeResponseDto>
    {
        public string Code { get; set; }
    }
}
