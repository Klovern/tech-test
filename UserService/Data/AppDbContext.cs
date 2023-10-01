
using Microsoft.EntityFrameworkCore;
using UserService.Objects;

namespace UserService.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> opt) : base(opt)
        {
        }

        public DbSet<User> Users { get; set; }
    }
}