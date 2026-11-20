using Microsoft.EntityFrameworkCore;
using RaceDay.API.Models;
using RaceDay.API.Services;

namespace RaceDay.API.Data
{
    // Handles initial data seeding for default system roles, categories, and admin users (Microsoft, 2024)
    public static class DbInitializer
    {
        public static async Task SeedAsync(RaceDayDbContext context, IAuthService authService)
        {
            // Ensure database is created (Microsoft, 2024)
            await context.Database.EnsureCreatedAsync();

            // Seed Roles if empty (Microsoft, 2024)
            if (!await context.Roles.AnyAsync())
            {
                var roles = new List<Role>
                {
                    new Role { RoleName = "Admin" },
                    new Role { RoleName = "Organizer" },
                    new Role { RoleName = "Participant" }
                };
                await context.Roles.AddRangeAsync(roles);
                await context.SaveChangesAsync();
            }

            // Seed Default Admin User if empty (Microsoft, 2024)
            var adminUser = await context.Users.FirstOrDefaultAsync(u => u.Email == "admin@raceday.com");
            if (adminUser == null)
            {
                var adminRole = await context.Roles.FirstAsync(r => r.RoleName == "Admin");
                adminUser = new User
                {
                    FullName = "System Administrator",
                    Email = "admin@raceday.com",
                    RoleId = adminRole.RoleId,
                    PasswordHash = authService.HashPassword("Admin123!")
                };
                await context.Users.AddAsync(adminUser);
                await context.SaveChangesAsync();
            }

            Event defaultEvent;
            if (!await context.Events.AnyAsync())
            {
                defaultEvent = new Event
                {
                    EventName = "Spring Championship 2026",
                    Description = "Annual regional championship race event.",
                    EventDate = DateTime.UtcNow.AddMonths(1),
                    Location = "Mooi River Valley",
                    OrganiserId = adminUser.UserId
                };
                await context.Events.AddAsync(defaultEvent);
                await context.SaveChangesAsync();
            }
            else
            {
                defaultEvent = await context.Events.FirstAsync();
            }

            // Seed Categories if empty (Microsoft, 2024)
            if (!await context.Categories.AnyAsync())
            {
                var categories = new List<Category>
                {
                    new Category { CategoryName = "5km Fun Run", Description = "5 kilometer entry-level running event", DistanceKM = 5.0m, EventId = defaultEvent.EventId },
                    new Category { CategoryName = "10km Road Race", Description = "10 kilometer road racing category", DistanceKM = 10.0m, EventId = defaultEvent.EventId },
                    new Category { CategoryName = "21km Half Marathon", Description = "21 kilometer competitive half marathon", DistanceKM = 21.1m, EventId = defaultEvent.EventId }
                };
                await context.Categories.AddRangeAsync(categories);
                await context.SaveChangesAsync();
            }
        }
    }
}