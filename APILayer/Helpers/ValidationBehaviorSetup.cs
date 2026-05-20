using DomainLayer.Models.Common;
using Microsoft.AspNetCore.Mvc;

namespace APILayer.Helpers
{
    /// <summary>
    /// Overrides ASP.NET Core's default 400 model validation response.
    ///
    /// By default, when a [Required] field is missing the framework returns a
    /// "ValidationProblemDetails" JSON shape that is different from OperationResult.
    /// This replaces that with OperationResult.Failure(...) so the frontend always
    /// receives the same error format regardless of where validation failed.
    /// </summary>
    public static class ValidationBehaviorSetup
    {
        public static IServiceCollection AddCustomValidationResponse(this IServiceCollection services)
        {
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = context =>
                {
                    var errors = context.ModelState.Values
                        .SelectMany(v => v.Errors)
                        .Select(e => e.ErrorMessage)
                        .ToArray();

                    var result = OperationResult<string>.Failure(errors);
                    return new BadRequestObjectResult(result);
                };
            });

            return services;
        }
    }
}
