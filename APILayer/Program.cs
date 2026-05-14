using System.Text;
using APILayer.Helpers;
using APILayer.Middleware;
using ApplicationLayer;
using ApplicationLayer.Auth;
using InfrastructureLayer;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace APILayer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // ── Application & Infrastructure ───────────────────────────────────────
            // Registers MediatR, AutoMapper, FluentValidation, and pipeline behaviors
            builder.Services.AddApplication();

            // Registers AppDbContext, all repositories, UserContextService, AuthService
            builder.Services.AddInfrastructure(builder.Configuration);

            // ── JWT Authentication ─────────────────────────────────────────────────
            builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("JwtSettings"));

            var jwtSettings = builder.Configuration.GetSection("JwtSettings").Get<JwtSettings>()!;
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(
                            Encoding.UTF8.GetBytes(jwtSettings.SecretKey)),
                        ValidateIssuer = true,
                        ValidIssuer = jwtSettings.Issuer,
                        ValidateAudience = true,
                        ValidAudience = jwtSettings.Audience,
                        ValidateLifetime = true,
                        ClockSkew = TimeSpan.Zero
                    };
                });

            // ── API ────────────────────────────────────────────────────────────────
            builder.Services.AddControllers();

            // Replaces the default 400 validation response with OperationResult format
            builder.Services.AddCustomValidationResponse();

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddOpenApi();

            // ── CORS ───────────────────────────────────────────────────────────────
            // AllowCredentials() is required for the HttpOnly refresh token cookie to work
            var corsOrigins = builder.Configuration
                .GetSection("CorsOrigins").Get<string[]>()
                ?? new[] { "http://localhost:5173", "http://localhost:3000" };

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowFrontend", policy =>
                {
                    policy.WithOrigins(corsOrigins)
                        .AllowCredentials()
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                });
            });

            // ── Build ──────────────────────────────────────────────────────────────
            var app = builder.Build();

            // ── Middleware pipeline (order matters) ────────────────────────────────

            // Must be first — wraps everything so no exception escapes as a raw 500 page
            app.UseMiddleware<ExceptionHandlingMiddleware>();

            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();

                // Serves Swagger UI. Fetches the generated spec, injects the Bearer security
                // scheme into it client-side, then hands the modified spec to Swagger UI —
                // this gives the standard Authorize button and lock icons without needing
                // Microsoft.OpenApi type imports (which changed incompatibly in v2).
                app.MapGet("/swagger", () => Results.Content("""
                    <!DOCTYPE html>
                    <html>
                    <head>
                        <title>SEEKAT API</title>
                        <meta charset="utf-8"/>
                        <meta name="viewport" content="width=device-width, initial-scale=1">
                        <link rel="stylesheet" href="https://unpkg.com/swagger-ui-dist@5/swagger-ui.css">
                    </head>
                    <body>
                    <div id="swagger-ui"></div>
                    <script src="https://unpkg.com/swagger-ui-dist@5/swagger-ui-bundle.js"></script>
                    <script>
                        fetch('/openapi/v1.json')
                            .then(r => r.json())
                            .then(spec => {
                                // Inject Bearer JWT security scheme so Swagger shows the Authorize button
                                spec.components = spec.components || {};
                                spec.components.securitySchemes = {
                                    Bearer: {
                                        type: 'http',
                                        scheme: 'bearer',
                                        bearerFormat: 'JWT',
                                        description: 'Paste the JWT token from POST /api/auth/login'
                                    }
                                };
                                spec.security = [{ Bearer: [] }];

                                SwaggerUIBundle({
                                    spec: spec,
                                    dom_id: '#swagger-ui',
                                    presets: [SwaggerUIBundle.presets.apis, SwaggerUIBundle.SwaggerUIStandalonePreset],
                                    layout: 'BaseLayout',
                                    persistAuthorization: true
                                });
                            });
                    </script>
                    </body>
                    </html>
                    """, "text/html"));
            }

            app.UseHttpsRedirection();

            // Must be before UseAuthentication and UseAuthorization
            app.UseCors("AllowFrontend");

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
