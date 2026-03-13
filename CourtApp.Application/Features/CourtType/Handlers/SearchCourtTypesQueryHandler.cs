
using AutoMapper;
using CourtApp.Application.Common;
using CourtApp.Application.Features.CourtType.Query;
using CourtApp.Application.Features.CourtType.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CourtApp.Application.Features.CourtType.Handlers
{
    public class SearchCourtTypesQueryHandler : IRequestHandler<SearchCourtTypesQuery, Result<PaginatedResult<GetCourtTypeResponse>>>
    {
        private readonly ICourtTypeRepository _repository;
        private readonly IMapper _mapper;
        private readonly ILogger<SearchCourtTypesQueryHandler> _logger;

        public SearchCourtTypesQueryHandler(
            ICourtTypeRepository repository,
            IMapper mapper,
            ILogger<SearchCourtTypesQueryHandler> logger)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<Result<PaginatedResult<GetCourtTypeResponse>>> Handle(SearchCourtTypesQuery request, CancellationToken cancellationToken)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(request.Keyword))
                    return Result<PaginatedResult<GetCourtTypeResponse>>.Fail("Search keyword cannot be empty.");

                var keyword = request.Keyword.Trim().ToLower();

                var query = _repository.CourtTypeEntities
                    .AsNoTracking()
                    .Where(x => x.CourtType.ToLower().Contains(keyword) || 
                                x.Abbreviation.ToLower().Contains(keyword));

                var totalCount = await query.CountAsync(cancellationToken);

                var courtTypes = await query
                    .OrderBy(x => x.CourtType)
                    .Skip((request.PageNumber - 1) * request.PageSize)
                    .Take(request.PageSize)
                    .ToListAsync(cancellationToken);

                var dtos = _mapper.Map<System.Collections.Generic.List<GetCourtTypeResponse>>(courtTypes);

                var paginatedResult = new PaginatedResult<GetCourtTypeResponse>
                {
                    Data = dtos,
                    TotalCount = totalCount,
                    PageNumber = request.PageNumber,
                    PageSize = request.PageSize,
                    TotalPages = (int)System.Math.Ceiling(totalCount / (double)request.PageSize)
                };

                return Result<PaginatedResult<GetCourtTypeResponse>>.Success(paginatedResult);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in SearchCourtTypesQueryHandler: {ex.Message}");
                return Result<PaginatedResult<GetCourtTypeResponse>>.Fail(ex.Message);
            }
        }
    }
}   