using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;


namespace CourtApp.Application.Common
{
    public class ApiResponse<T>
    {
        public bool Status { get; init; }
        public string? Message { get; init; }
        public int StatusCode { get; init; }
        public DateTime Timestamp { get; init; } = DateTime.UtcNow;

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public T? Data { get; init; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public PaginationMeta? Pagination { get; init; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public List<string>? Errors { get; init; }

        // ── From Result<T> ────────────────────────────────────────────
        public static ApiResponse<T> FromResult(Result<T> result, int successCode = 200)
            => result.Succeeded
                ? new()
                {
                    Status = true,
                    Message = result.Message ?? "Success",
                    StatusCode = successCode,
                    Data = result.Data
                }
                : new()
                {
                    Status = false,
                    Message = result.Message ?? "An error occurred",
                    StatusCode = 400,
                    Errors = result.Errors.Count > 0 ? result.Errors : null
                };

        // ── From Result (non-generic — commands) ──────────────────────
        public static ApiResponse<T> FromResult(Result result, int successCode = 200)
            => result.Succeeded
                ? new()
                {
                    Status = true,
                    Message = result.Message ?? "Success",
                    StatusCode = successCode
                }
                : new()
                {
                    Status = false,
                    Message = result.Message ?? "An error occurred",
                    StatusCode = 400,
                    Errors = result.Errors.Count > 0 ? result.Errors : null
                };

        // ── From PaginatedResult — flat, no nesting ───────────────────
        public static ApiResponse<List<TItem>> FromPaginated<TItem>(
            PaginatedResult<TItem> result, int successCode = 200)
            => result.Succeeded
                ? new ApiResponse<List<TItem>>
                {
                    Status = true,
                    Message = result.Message ?? "Success",
                    StatusCode = successCode,
                    Data = result.Data.ToList(),
                    Pagination = result.Pagination
                }
                : new ApiResponse<List<TItem>>
                {
                    Status = false,
                    Message = result.Message ?? "An error occurred",
                    StatusCode = 400,
                    Errors = result.Errors.ToList().Count > 0 ? result.Errors.ToList() : null
                };

        // ── Direct factories ──────────────────────────────────────────
        public static ApiResponse<T> Success(T? data, string message = "Success", int statusCode = 200)
            => new() { Status = true, Message = message, Data = data, StatusCode = statusCode };

        public static ApiResponse<T> Failure(string message, int statusCode = 400, List<string>? errors = null)
            => new() { Status = false, Message = message, StatusCode = statusCode, Errors = errors };

        public static ApiResponse<T> ServerError(string message = "An unexpected error occurred",
            List<string>? errors = null)
            => new() { Status = false, Message = message, StatusCode = 500, Errors = errors };
    }
}
