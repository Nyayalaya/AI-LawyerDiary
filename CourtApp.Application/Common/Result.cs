using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourtApp.Application.Common
{
    public class Result
    {
        public bool Succeeded { get; protected set; }
        public string? Message { get; protected set; }
        public List<string> Errors { get; protected set; } = new();

        public static Result Fail(string? message = null, List<string>? errors = null)
            => new() { Succeeded = false, Message = message, Errors = errors ?? new() };

        public static Task<Result> FailAsync(string? message = null, List<string>? errors = null)
            => Task.FromResult(Fail(message, errors));

        public static Result Success(string? message = null)
            => new() { Succeeded = true, Message = message };

        public static Task<Result> SuccessAsync(string? message = null)
            => Task.FromResult(Success(message));
    }

    /// <summary>Generic Result — for queries that return data</summary>
    public class Result<T> : Result
    {
        public T? Data { get; private set; }

        public static Result<T> Fail(string? message = null, List<string>? errors = null)
            => new() { Succeeded = false, Message = message, Errors = errors ?? new() };

        public static new Task<Result<T>> FailAsync(string? message = null, List<string>? errors = null)
            => Task.FromResult(Fail(message, errors));

        public static Result<T> Success(T? data = default, string? message = null)
            => new() { Succeeded = true, Data = data, Message = message };

        public static new Task<Result<T>> SuccessAsync(T? data = default, string? message = null)
            => Task.FromResult(Success(data, message));

        public static implicit operator Result<T>(T? data) => Success(data);
        public static implicit operator T?(Result<T> result) => result.Data;
    }

}
