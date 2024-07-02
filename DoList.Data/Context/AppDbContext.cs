
using DoList.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace DoList.Data.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> dbContextOptions) : base(options: dbContextOptions)
        {
            
        }

        public DbSet<Users> Users { get; set; }
        public DbSet<Tasks> Tasks { get; set; }
    }
}
