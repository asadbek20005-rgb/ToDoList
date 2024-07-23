
using DoList.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DoList.Data.Context
{
    public class AppDbContext : IdentityDbContext<IdentityUser> 
    {
        public AppDbContext(DbContextOptions<AppDbContext> dbContextOptions) : base(options: dbContextOptions)
        {
            
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies();
        }

        public  DbSet<Users> Users { get; set; }
        public DbSet<Tasks> Tasks { get; set; }

       
    }
}
