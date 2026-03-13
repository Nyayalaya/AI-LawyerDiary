using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourtApp.Application.Common
{
    public class Result<T>
    {
        public bool Succeeded { get; set; }
        public string? Message { get; set; }
        public T? Data { get; set; }

        // Fail
        public static Result<T> Fail(string? message = null)
        {
            return new Result<T> { Succeeded = false, Message = message };
        }

        public static Task<Result<T>> FailAsync(string? message = null)
        {
            return Task.FromResult(Fail(message));
        }

        // Success
        public static Result<T> Success(T? data = default, string? message = null)
        {
            return new Result<T> { Succeeded = true, Data = data, Message = message };
        }

        public static Task<Result<T>> SuccessAsync(T? data = default, string? message = null)
        {
            return Task.FromResult(Success(data, message));
        }
    }

}
