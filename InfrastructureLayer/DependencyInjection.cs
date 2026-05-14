using ApplicationLayer.AdvertisementImages.Interfaces;
using ApplicationLayer.Auth;
using ApplicationLayer.Auth.Interfaces;
using ApplicationLayer.Cat.Interfaces;
using ApplicationLayer.CatReport.Interfaces;
using ApplicationLayer.Comments.Interfaces;
using ApplicationLayer.Common.Interfaces;
using ApplicationLayer.Location.Interfaces;
using ApplicationLayer.SavedAdvertisements.Interfaces;
using ApplicationLayer.Users.Interfaces;
using InfrastructureLayer.Database;
using InfrastructureLayer.Repositories.Accounts;
using InfrastructureLayer.Repositories.Advertisements;
using InfrastructureLayer.Repositories.AdvertisementImages;
using InfrastructureLayer.Repositories.Cats;
using InfrastructureLayer.Repositories.Comments;
using InfrastructureLayer.Repositories.Locations;
using InfrastructureLayer.Repositories.SavedAdvertisements;
using InfrastructureLayer.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace InfrastructureLayer
{
    public static class DependencyInjection
    {
        /// <summary>
        /// Registers all Infrastructure layer services.
        /// Called once in Program.cs — keeps database and repository wiring out of the API layer.
        /// </summary>
        public static IServiceCollection AddInfrastructure(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            // Register AppDbContext with the SQL Server connection string from appsettings.json
            var connectionString = configuration.GetConnectionString("DefaultConnection")
                ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(connectionString));

            // Bind every repository interface to its concrete EF Core implementation.
            // Scoped = one instance per HTTP request, shared across all handlers in that request.
            services.AddScoped<IAccountRepository, AccountRepository>();
            services.AddScoped<ICatRepository, CatRepository>();
            services.AddScoped<IAdvertisementRepository, AdvertisementRepository>();
            services.AddScoped<ICommentRepository, CommentRepository>();
            services.AddScoped<ILocationRepository, LocationRepository>();
            services.AddScoped<IAdvertisementImageRepository, AdvertisementImageRepository>();
            services.AddScoped<ISavedAdvertisementRepository, SavedAdvertisementRepository>();

            // Auth and HTTP context services
            services.AddHttpContextAccessor();
            services.AddScoped<IUserContextService, UserContextService>();
            services.AddScoped<IAuthService, AuthService>();

            return services;
        }
    }
}
