using CourtApp.Application.Common;
using CourtApp.Application.Features.CourtDistrict.DTOs;
using CourtApp.Application.Features.CourtDistrict.Query;
using CourtApp.Application.Interfaces.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CourtApp.Application.Features.CourtDistrict.Handlers
{
    public class GetCourtDistrictDropdownQueryHandler : IRequestHandler<GetCourtDistrictDropdownQuery, PaginatedResult<CourtDistrictDropdownResponse>>
    {
        private readonly ICourtDistrictRepository _repository;

        public GetCourtDistrictDropdownQueryHandler(ICourtDistrictRepository repository)
        {
            _repository = repository;
        }

        public async Task<PaginatedResult<CourtDistrictDropdownResponse>> Handle(
            GetCourtDistrictDropdownQuery request, CancellationToken cancellationToken)
        {
            // Build query with State include
            var query = _repository.Entities
                .Include(d => d.State)
                .AsQueryable();

            // Apply search filter if provided
            if (!string.IsNullOrEmpty(request.SearchTerm))
            {
                var searchTerm = request.SearchTerm.ToLower().Trim();
                query = query.Where(d => d.Name.ToLower().Contains(searchTerm) ||
                                        d.State.Name.ToLower().Contains(searchTerm));
            }

            // Get total count before pagination
            var totalCount = await query.CountAsync(cancellationToken);

            // Apply pagination if PageSize is specified
            if (request.PageSize.HasValue && request.PageSize > 0)
            {
                var skipValue = request.Skip ?? 0;
                query = query
                    .Skip(skipValue)
                    .Take(request.PageSize.Value);
            }

            // Execute query and map to DTO
            var districts = await query
                .OrderBy(d => d.Name)
                .Select(d => new CourtDistrictDropdownResponse
                {
                    Id = d.Id,
                    Name = d.Name,
                    StateName = d.State.Name
                })
                .ToListAsync(cancellationToken);

            // Calculate page number based on skip and page size
            int pageNumber = 1;
            int pageSize = request.PageSize ?? totalCount;

            if (request.PageSize.HasValue && request.PageSize > 0)
            {
                pageNumber = (request.Skip ?? 0) / request.PageSize.Value + 1;
            }

            // Return using PaginatedResult.Success factory method
            return PaginatedResult<CourtDistrictDropdownResponse>.Success(
                districts,
                totalCount,
                pageNumber,
                pageSize,
                "Court districts retrieved successfully.");
        }
    }
}
