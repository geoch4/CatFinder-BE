using ApplicationLayer.Common.Behaviors;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace ApplicationLayer
{
    public static class DependencyInjection
    {
        /// <summary>
        /// Registers all Application layer services.
        /// Called once in Program.cs — keeps all wiring out of the API layer.
        /// </summary>
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            var assembly = typeof(DependencyInjection).Assembly;

            // Scans the assembly and registers all IRequestHandler implementations
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(assembly));

            // Scans the assembly and registers all AutoMapper Profile classes
            services.AddAutoMapper(cfg => cfg.AddMaps(assembly));

            // Scans the assembly and registers all AbstractValidator<T> classes
            services.AddValidatorsFromAssembly(assembly);

            // Runs FluentValidation before every handler — returns OperationResult.Failure on invalid input
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

            // Logs every request name, payload, and execution time automatically
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));

            return services;
        }
    }
}
