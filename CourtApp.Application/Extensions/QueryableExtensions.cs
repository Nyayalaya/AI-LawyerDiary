using CourtApp.Application.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;

namespace CourtApp.Application.Extensions
{
    public static class PaginationExtensions
    {
        #region Async Pagination for IQueryable (EF Core)

        public static async Task<PaginatedResult<T>> ToPaginatedListAsync<T>(
            this IQueryable<T> source,
            int pageNumber,
            int pageSize)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            if (pageNumber <= 0) pageNumber = 1;
            if (pageSize <= 0) pageSize = 10;

            var totalCount = await source.LongCountAsync();
            var items = await source.Skip((pageNumber - 1) * pageSize)
                                    .Take(pageSize)
                                    .ToListAsync();

            return new PaginatedResult<T>(items, (int)totalCount, pageNumber, pageSize);
        }

        #endregion

        #region Sync Pagination for IEnumerable (In-memory Lists / Cache)

        public static PaginatedResult<T> ToPaginatedResult<T>(
            this IEnumerable<T> source,
            int pageNumber,
            int pageSize)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            if (pageNumber <= 0) pageNumber = 1;
            if (pageSize <= 0) pageSize = 10;

            var totalCount = source.Count();
            var items = source.Skip((pageNumber - 1) * pageSize)
                              .Take(pageSize)
                              .ToList();

            return new PaginatedResult<T>(items, totalCount, pageNumber, pageSize);
        }

        #endregion

        #region Dynamic Ordering Extensions

        public static IQueryable<T> OrderByDynamic<T>(this IQueryable<T> source, string propertyName)
        {
            return ApplyOrder(source, propertyName, "OrderBy");
        }

        public static IQueryable<T> OrderByDescendingDynamic<T>(this IQueryable<T> source, string propertyName)
        {
            return ApplyOrder(source, propertyName, "OrderByDescending");
        }

        private static IQueryable<T> ApplyOrder<T>(IQueryable<T> source, string propertyName, string methodName)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            if (string.IsNullOrWhiteSpace(propertyName))
                return source;

            Type entityType = typeof(T);
            PropertyInfo property = entityType.GetProperty(propertyName,
                BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);

            if (property == null)
                return source; // fallback if property not found

            var parameter = Expression.Parameter(entityType, "x");
            var propertyAccess = Expression.Property(parameter, property);
            var orderByExpression = Expression.Lambda(propertyAccess, parameter);

            var resultExp = Expression.Call(
                typeof(Queryable),
                methodName,
                new Type[] { entityType, property.PropertyType },
                source.Expression,
                Expression.Quote(orderByExpression)
            );

            return source.Provider.CreateQuery<T>(resultExp);
        }

        #endregion
    }
}
