using System;
using System.Collections.Generic;

namespace CourtApp.Application.Common
{
    public class PaginatedResult<T>
    {
        public bool Succeeded { get; private set; }
        public string? Message { get; private set; }
        public List<string> Errors { get; private set; } = new();
        public List<T> Data { get; private set; } = new();
        public PaginationMeta Pagination { get; private set; } = default!;

        private PaginatedResult() { }

        public static PaginatedResult<T> Success(
            List<T> data,
            int totalCount,
            int pageNumber,
            int pageSize,
            string message = "Success")
            => new()
            {
                Succeeded = true,
                Message = message,
                Data = data,
                Pagination = PaginationMeta.Create(totalCount, pageNumber, pageSize)
            };

        public static PaginatedResult<T> Failure(string message, List<string>? errors = null)
            => new()
            {
                Succeeded = false,
                Message = message,
                Errors = errors ?? new()
            };
    }
}
