using Microsoft.EntityFrameworkCore;
using Todo.Infrastructure.Configuration;

namespace Todo.Infrastructure.ApplicationContext;
public class ApplicationDbContext : DbContext
{
    public DbSet<Todo.Core.Entities.Todo> Todos { get; set; }
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(TodoConfiguration).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}