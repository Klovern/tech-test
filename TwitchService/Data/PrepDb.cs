using Microsoft.EntityFrameworkCore;

namespace TwitchService.Data
{
    public static class PrepDb
    {
        public static void PrepPopulation(this IApplicationBuilder app, bool isProd)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                SeedData(serviceScope.ServiceProvider.GetService<AppDbContext>(), isProd);
            }
        }

        private static void SeedData(AppDbContext context, bool isProd)
        {
            if (isProd)
            {
                Console.WriteLine("--> Attempting to apply migrations...");
                try
                {
                    context.Database.Migrate();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"--> Could not run migrations: {ex.Message}");
                }
            }

            if (!context.TwitchUsers.Any())
            {
                Console.WriteLine("--> Seeding Data...");

                context.TwitchUsers.AddRange(
                    new Objects.TwitchUser { Id = 1, LinkId = 1, TwitchUserId = 2492 }
                );

                context.SaveChanges();
            }
            else
            {
                Console.WriteLine("--> We already have data");
            }
        }
    }
}