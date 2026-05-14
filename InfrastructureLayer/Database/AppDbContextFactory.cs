using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace InfrastructureLayer.Database
{
    /// <summary>
    /// Used by EF Core tooling (dotnet ef migrations add / database update) at design time.
    /// Without this, the CLI can't create an AppDbContext instance because
    /// it doesn't have access to the running app's DI container.
    /// </summary>
    public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();

            // This connection string is only used when running EF Core CLI commands locally.
            // The real connection string is provided via appsettings.json at runtime.
            optionsBuilder.UseSqlServer(
                "Server=(localdb)\\mssqllocaldb;Database=CatFinderDb;Trusted_Connection=True;");

            return new AppDbContext(optionsBuilder.Options);
        }
    }
}
