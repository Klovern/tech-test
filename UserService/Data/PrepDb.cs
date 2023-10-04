using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using Microsoft.IdentityModel.Tokens;
using UserService.Objects;

namespace UserService.Data
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

            if (!context.Users.Any())
            {
                Console.WriteLine("--> Seeding Data...");

                context.Users.AddRange(
                    new User { Id = 1, Email = "oskar.kindeland@gmail.com", FirstName = "Oskar", LastName = "Kindeland", },
                    new User { Id = 2, Email = "tom.niskanen@lurkit.se", FirstName = "Tom", LastName = "Niskanen" }
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
