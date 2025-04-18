using Microsoft.EntityFrameworkCore;
using ToDoList.Data.Entites;

namespace ToDoList.Data.DbContexts;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }
    public DbSet<User> Users { get; set; }
}
