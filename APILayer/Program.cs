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
