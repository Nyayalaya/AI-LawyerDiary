using CourtApp.Application.Common;
using CourtApp.Application.Extensions;
using CourtApp.Application.Features.CourtHall.DTOs;
using CourtApp.Application.Features.CourtHall.Interfaces;
using CourtApp.Application.Features.CourtHall.Query;
using CourtApp.Domain.Entities.Masters;
using KT3Core.Areas.Global.Classes;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace CourtApp.Application.Features.CourtHall.Handlers
{
    public class GetCourtHallQueryHandler : IRequestHandler<GetCourtHallQuery, PaginatedResult<CourtHallResponse>>
    {
        private readonly ICourtHallRepository repository;

        public GetCourtHallQueryHandler(ICourtHallRepository repository)
        {
            this.repository = repository;
        }

        public async Task<PaginatedResult<CourtHallResponse>> Handle(GetCourtHallQuery request, CancellationToken cancellationToken)
        {
            Expression<Func<CourtHallEntity, CourtHallResponse>> expression = e => new CourtHallResponse
            {
                Id = e.Id,
                Name = e.Name,
               
                JudgeName = e.JudgeName,
                RoomNumber = e.RoomNumber,
                CourtComplexId = e.CourtComplexId,
                CourtComplexName = e.CourtComplex != null ? e.CourtComplex.Name : string.Empty,
                Languages = e.Languages
            };

            var predicate = PredicateBuilder.True<CourtHallEntity>();

            if (request.CourtComplexId != Guid.Empty)
                predicate = predicate.And(x => x.CourtComplexId == request.CourtComplexId);

            try
            {
                var paginatedList = await repository.Entities
                    .Include(c => c.CourtComplex)
                    .Include(c => c.Languages)
                    .Where(predicate)
                    .OrderBy(o => o.Name)
                    .Select(expression)
                    .ToPaginatedListAsync(request.PageNumber, request.PageSize);

                return paginatedList;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
            }

            return null;
        }
    }
}
