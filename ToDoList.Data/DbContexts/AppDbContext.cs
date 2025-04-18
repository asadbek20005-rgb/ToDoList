using Microsoft.EntityFrameworkCore;
using ToDoList.Data.Entites;
using Task = ToDoList.Data.Entites.Task;

namespace ToDoList.Data.DbContexts;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }
    public DbSet<User> Users { get; set; }
    public DbSet<Task> Tasks { get; set; }
}
