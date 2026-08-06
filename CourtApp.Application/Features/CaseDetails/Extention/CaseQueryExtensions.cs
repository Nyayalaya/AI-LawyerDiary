using CourtApp.Domain.Entities.CaseDetails;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CourtApp.Application.Features.CaseDetails.Extention
{
    public static class CaseQueryExtensions
    {
        public static IQueryable<CaseEntity> ApplyDefaultOrdering(
            this IQueryable<CaseEntity> query)
        {
            return query
                .OrderByDescending(x =>
                    x.NextDate ??
                    x.CaseProceedingEntities
                        .OrderByDescending(cp => cp.NextDate)
                        .Select(cp => cp.NextDate)
                        .FirstOrDefault())
                .ThenByDescending(x => x.InstitutionDate)
                .ThenByDescending(x => x.CreatedOn);
        }

        public static IQueryable<CaseEntity> ApplyCaseAccessFilter(
            this IQueryable<CaseEntity> query,
            List<string> linkedIds)
        {
            if (linkedIds == null || !linkedIds.Any())
                return query;

            return query.Where(c =>
                linkedIds.Contains(c.CreatedBy) ||
                c.CaseAssignedEntities.Any(a =>
                    linkedIds.Contains(a.LawyerId)));
        }


        public static IQueryable<T> ApplyFilters<T>(
        this IQueryable<T> query,
        Expression<Func<T, bool>> predicate)
        {
            return query.Where(predicate);
        }
    }
}
