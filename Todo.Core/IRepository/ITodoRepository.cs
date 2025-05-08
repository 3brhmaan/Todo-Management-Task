using Todo.Core.RequestOptions;

namespace Todo.Core.IRepository;
public interface ITodoRepository
{
    void CreateTodo(Entities.Todo todo);
    void DeleteTodo(Entities.Todo todo);
    Task<IEnumerable<Entities.Todo>> GelAllTodoAsync(RequestParameter requestParamter);
    Task<Entities.Todo?> GetTodoByIdAsync(Guid id , bool trackChanges);
}

