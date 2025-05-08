using Todo.Core.IRepository;
using Todo.Infrastructure.ApplicationContext;

namespace Todo.Infrastructure.Repository;
public class RepositoryManager : IRepositoryManager
{
    private readonly ApplicationDbContext dbContext;
    private readonly Lazy<ITodoRepository> todoRepository;

    public RepositoryManager(ApplicationDbContext dbContext)
    {
        this.dbContext = dbContext;
        todoRepository = new Lazy<ITodoRepository>(() => new TodoRepository(dbContext));
    }

    public ITodoRepository Todo => todoRepository.Value;

    public async Task SaveAsync()
    {
        await dbContext.SaveChangesAsync();
    }
}
