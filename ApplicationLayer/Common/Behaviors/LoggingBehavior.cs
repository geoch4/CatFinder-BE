using System.Diagnostics;
using MediatR;
using Microsoft.Extensions.Logging;

namespace ApplicationLayer.Common.Behaviors
{
    /// <summary>
    /// MediatR pipeline behavior that logs every request and its execution time automatically.
    ///
    /// Runs around every command and query — no logging code needed inside handlers.
    /// Logs the request name and payload on entry, elapsed time on success,
    /// and the exception details if the handler throws.
    /// </summary>
    public class LoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        private readonly ILogger<LoggingBehavior<TRequest, TResponse>> _logger;

        public LoggingBehavior(ILogger<LoggingBehavior<TRequest, TResponse>> logger)
        {
            _logger = logger;
        }

        public async Task<TResponse> Handle(
            TRequest request,
            RequestHandlerDelegate<TResponse> next,
            CancellationToken cancellationToken)
        {
            var requestName = typeof(TRequest).Name;
            var stopwatch = Stopwatch.StartNew();

            _logger.LogInformation(
                "Handling {RequestName} | Payload: {@Request}", requestName, request);

            try
            {
                var response = await next();
                stopwatch.Stop();

                _logger.LogInformation(
                    "Handled {RequestName} in {ElapsedMs}ms", requestName, stopwatch.ElapsedMilliseconds);

                return response;
            }
            catch (Exception ex)
            {
                stopwatch.Stop();

                _logger.LogError(ex,
                    "Unhandled exception in {RequestName} after {ElapsedMs}ms", requestName, stopwatch.ElapsedMilliseconds);

                throw;
            }
        }
    }
}
