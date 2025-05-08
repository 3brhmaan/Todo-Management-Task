using Microsoft.EntityFrameworkCore;
using Todo.Core.Enums;
using Todo.Core.IRepository;
using Todo.Core.RequestOptions;
using Todo.Infrastructure.ApplicationContext;
using Todo.Infrastructure.Extensions;

namespace Todo.Infrastructure.Repository;
public class TodoRepository
    : RepositoryBase<Core.Entities.Todo>, ITodoRepository
{
    private readonly ApplicationDbContext dbContext;

    public TodoRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task<IEnumerable<Core.Entities.Todo>> GelAllTodoAsync(RequestParameter requestParamter)
    {
        var query = FindAll(false);

        if (requestParamter.Status != TodoStatus.All)
        {
            query = query.Where(x => x.Status == requestParamter.Status);
        }

        if (requestParamter.Priority != TodoPriority.All)
        {
            query = query.Where(x => x.Priority == requestParamter.Priority);
        }

        if (requestParamter.StartDate != default)
        {
            query = query.Where(x => x.DueDate >= requestParamter.StartDate);
        }

        if (requestParamter.EndDate != default)
        {
            query = query.Where(x => x.DueDate <= requestParamter.EndDate);
        }

        if (requestParamter.OrderBy != null)
        {
            query = query.Sort(requestParamter.OrderBy);
        }

        return await query.ToListAsync();
    }

    public async Task<Core.Entities.Todo?> GetTodoByIdAsync(Guid id , bool trackChanges)
    {
        return await FindByCondition(x => x.Id == id , trackChanges)
            .FirstOrDefaultAsync();
    }

    public void CreateTodo(Core.Entities.Todo todo)
    {
        Create(todo);
    }

    public void DeleteTodo(Core.Entities.Todo todo)
    {
        Delete(todo);
    }
}
