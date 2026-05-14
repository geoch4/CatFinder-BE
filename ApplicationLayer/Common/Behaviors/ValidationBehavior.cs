using DomainLayer.Models.Common;
using FluentValidation;
using MediatR;

namespace ApplicationLayer.Common.Behaviors
{
    /// <summary>
    /// MediatR pipeline behavior that runs FluentValidation validators automatically
    /// before every command or query handler executes.
    ///
    /// If validation fails it returns OperationResult.Failure(...) with all error messages
    /// instead of letting the request reach the handler — no try/catch needed in handlers.
    /// </summary>
    public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
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
            // No validators registered for this request — skip straight to the handler
            if (!_validators.Any())
                return await next();

            var context = new ValidationContext<TRequest>(request);

            var validationResults = await Task.WhenAll(
                _validators.Select(v => v.ValidateAsync(context, cancellationToken)));

            var failures = validationResults
                .SelectMany(r => r.Errors)
                .Where(f => f != null)
                .ToList();

            if (!failures.Any())
                return await next();

            var errors = failures.Select(f => f.ErrorMessage).ToArray();

            // If TResponse is OperationResult<T>, call its Failure factory via reflection
            // so the controller receives a clean error response instead of an exception
            var responseType = typeof(TResponse);
            var failureMethod = responseType.GetMethod("Failure", new[] { typeof(string[]) });

            if (failureMethod != null)
                return (TResponse)failureMethod.Invoke(null, new object[] { errors })!;

            // Fallback for any handler that doesn't use OperationResult
            throw new ValidationException(failures);
        }
    }
}
