using Microsoft.EntityFrameworkCore;
using TwitchService.Objects;

namespace TwitchService.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> opts) : base(opts)
        {

        }

        public DbSet<TwitchUser> TwitchUsers { get; set; }
    }
}
