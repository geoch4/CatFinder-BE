using DomainLayer.Models;
using InfrastructureLayer.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace InfrastructureLayer.Seeding
{
    public static class DbSeeder
    {
        public static async Task SeedAdminAsync(IServiceProvider services)
        {
            var db = services.GetRequiredService<AppDbContext>();

            const string adminEmail = "admin@catfinder.se";
            if (await db.Accounts.AnyAsync(a => a.Email == adminEmail))
                return;

            db.Accounts.Add(new Account
            {
                Username = "admin",
                Email = adminEmail,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("Admin1234!"),
                Role = Role.Admin,
                CreatedAt = DateTime.UtcNow
            });
            await db.SaveChangesAsync();
        }
    }
}
