// CourtApp.Application/Extensions/PaginationExtensions.cs
using CourtApp.Application.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace CourtApp.Application.Extensions
{
    public static class PaginationExtensions
    {
        // ── Async Pagination — IQueryable (EF Core) ───────────────────

        /// <summary>
        /// Paginates an EF Core IQueryable asynchronously.
        /// Always call after filtering/ordering, before materializing.
        /// </summary>
        public static async Task<PaginatedResult<T>> ToPaginatedListAsync<T>(
            this IQueryable<T> source,
            int pageNumber,
            int pageSize,
            CancellationToken cancellationToken = default)
        {
            if (source is null)
                throw new ArgumentNullException(nameof(source));

            if (pageNumber <= 0) pageNumber = 1;
            if (pageSize <= 0) pageSize = 10;

            // Single round-trip: count + data in separate but sequential awaits
            // (EF Core does not support both in one query without raw SQL)
            var totalCount = await source.CountAsync(cancellationToken);

            if (totalCount == 0)
                return PaginatedResult<T>.Success(
                    new List<T>(), 0, pageNumber, pageSize);

            var items = await source
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(cancellationToken);

            return PaginatedResult<T>.Success(items, totalCount, pageNumber, pageSize);
        }

        // ── Sync Pagination — IEnumerable (In-memory / Cache) ─────────

        /// <summary>
        /// Paginates an in-memory list or cached collection synchronously.
        /// Materializes once to avoid double enumeration.
        /// </summary>
        public static PaginatedResult<T> ToPaginatedResult<T>(
            this IEnumerable<T> source,
            int pageNumber,
            int pageSize)
        {
            if (source is null)
                throw new ArgumentNullException(nameof(source));

            if (pageNumber <= 0) pageNumber = 1;
            if (pageSize <= 0) pageSize = 10;

            // Materialize once — avoids double enumeration on Count() + Skip/Take
            var list = source as List<T> ?? source.ToList();
            var totalCount = list.Count;

            if (totalCount == 0)
                return PaginatedResult<T>.Success(
                    new List<T>(), 0, pageNumber, pageSize);

            var items = list
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            return PaginatedResult<T>.Success(items, totalCount, pageNumber, pageSize);
        }

        // ── Dynamic Ordering — IQueryable ─────────────────────────────

        // Expression cache — avoids reflection hit on every call
        private static readonly ConcurrentDictionary<string, LambdaExpression>
            _orderCache = new();

        /// <summary>
        /// Orders an IQueryable by a property name string ascending.
        /// Property name is case-insensitive. Falls back to original if not found.
        /// </summary>
        public static IQueryable<T> OrderByDynamic<T>(
            this IQueryable<T> source, string propertyName)
            => ApplyOrder(source, propertyName, "OrderBy");

        /// <summary>
        /// Orders an IQueryable by a property name string descending.
        /// Property name is case-insensitive. Falls back to original if not found.
        /// </summary>
        public static IQueryable<T> OrderByDescendingDynamic<T>(
            this IQueryable<T> source, string propertyName)
            => ApplyOrder(source, propertyName, "OrderByDescending");

        private static IQueryable<T> ApplyOrder<T>(
            IQueryable<T> source,
            string propertyName,
            string methodName)
        {
            if (source is null)
                throw new ArgumentNullException(nameof(source));

            if (string.IsNullOrWhiteSpace(propertyName))
                return source;

            var cacheKey = $"{typeof(T).FullName}.{propertyName}";
            var entityType = typeof(T);

            var lambda = _orderCache.GetOrAdd(cacheKey, _ =>
            {
                var property = entityType.GetProperty(
                    propertyName,
                    BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);

                if (property is null) return null!;

                var parameter = Expression.Parameter(entityType, "x");
                var propertyAccess = Expression.Property(parameter, property);
                return Expression.Lambda(propertyAccess, parameter);
            });

            // Property not found — return source unchanged
            if (lambda is null) return source;

            var resultExp = Expression.Call(
                typeof(Queryable),
                methodName,
                new[] { entityType, lambda.Body.Type },
                source.Expression,
                Expression.Quote(lambda));

            return source.Provider.CreateQuery<T>(resultExp);
        }
    }
}