using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace CourtApp.Application.Common.Behaviors
{
    /// <summary>
    /// MediatR Pipeline Behavior for validating requests before they reach handlers
    /// Returns validation errors in a structured format instead of throwing exceptions
    /// </summary>
    public class ValidationBehavior<TRequest, TResponse>
        : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }

        public async Task<TResponse> Handle(
            TRequest request,
            RequestHandlerDelegate<TResponse> next,
            CancellationToken cancellationToken)
        {
            // Skip validation if no validators registered
            if (!_validators.Any())
                return await next();

            var context = new ValidationContext<TRequest>(request);

            // Run all validators in parallel
            var validationResults = await Task.WhenAll(
                _validators.Select(v => v.ValidateAsync(context, cancellationToken)));

            // Collect all validation errors
            var failures = validationResults
                .SelectMany(r => r.Errors)
                .Where(f => f != null)
                .ToList();

            // If validation passes, proceed to handler
            if (!failures.Any())
                return await next();

            // Extract error messages
            var errorMessages = failures
                .GroupBy(x => x.PropertyName)
                .ToDictionary(
                    g => g.Key,
                    g => g.Select(x => x.ErrorMessage).ToList())
                .SelectMany(x => x.Value)
                .ToList();

            // Return validation error response based on TResponse type
            return HandleValidationFailure<TResponse>(errorMessages);
        }

        /// <summary>
        /// Creates an appropriate error response based on the response type
        /// Handles both Result and Result&lt;T&gt; types
        /// </summary>
        private TResponse HandleValidationFailure<T>(List<string> errors)
        {
            var responseType = typeof(TResponse);

            // Check if response type is Result
            if (responseType == typeof(Result))
            {
                return (TResponse)(object)Result.Fail("Validation failed", errors);
            }

            // Handle Result<T> generic type
            if (responseType.IsGenericType &&
                responseType.GetGenericTypeDefinition() == typeof(Result<>))
            {
                var genericType = responseType.GetGenericArguments()[0];
                var failMethod = typeof(Result<>)
                    .MakeGenericType(genericType)
                    .GetMethod("Fail", 
                        BindingFlags.Public | BindingFlags.Static,
                        null,
                        new[] { typeof(string), typeof(List<string>) },
                        null);

                if (failMethod != null)
                {
                    return (TResponse)(object)failMethod.Invoke(null, 
                        new object[] { "Validation failed", errors })!;
                }
            }

            // Fallback: throw validation exception if unable to create proper response
            throw new ValidationException(
                errors.Select((msg, idx) => 
                    new FluentValidation.Results.ValidationFailure(
                        $"Field_{idx}", 
                        msg)).ToList());
        }
    }
}
