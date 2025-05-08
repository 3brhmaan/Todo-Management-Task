using Todo.Core.RequestOptions;
using Todo.Service.DataTransferObjects;

namespace Todo.Service.IService;
public interface ITodoService
{
    Task<TodoDto> CreateTodoAsync(TodoForCreatinDto todoForCreatin);
    Task DeleteTodoAsync(Guid id , bool trackChanges);
    Task<IEnumerable<TodoDto>> GetAllTodoAsync(RequestParameter requestParamter);
    Task<TodoDto> GetTodoByIdAsync(Guid id , bool trackChanges);
    Task<Core.Entities.Todo> GetTodoEntityByIdAsync(Guid id , bool trackChanges);
    Task SaveChanges();
    Task UpdateTodoAsync(TodoForUpdatingDto todoForUpdating , Guid id , bool trackChanges);
}
