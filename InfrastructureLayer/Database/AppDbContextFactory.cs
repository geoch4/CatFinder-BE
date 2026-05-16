using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace InfrastructureLayer.Database
{
    /// <summary>
    /// Used by EF Core tooling (dotnet ef migrations add / database update)
    /// to create an AppDbContext instance at design time.
    /// The connection string is loaded from appsettings.json
    /// and appsettings.Development.json instead of being hardcoded.
    /// This keeps local database settings and secrets out of source control.
    /// </summary>
    public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args)
        {
            // Path to the API/startup project where appsettings files are located
            var basePath = Path.Combine(Directory.GetCurrentDirectory(), "..", "APILayer");

            // Builds configuration from appsettings files
            var configuration = new ConfigurationBuilder()
                .SetBasePath(basePath)
                .AddJsonFile("appsettings.json", optional: false)
                .AddJsonFile("appsettings.Development.json", optional: true)
                .Build();

            // Reads the SQL Server connection string from configuration
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            // Configures Entity Framework to use SQL Server
            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
            optionsBuilder.UseSqlServer(connectionString);

            // Returns a configured AppDbContext instance
            return new AppDbContext(optionsBuilder.Options);
        }
    }
}
