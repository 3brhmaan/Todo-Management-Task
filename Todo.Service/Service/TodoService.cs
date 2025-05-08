using AutoMapper;
using Todo.Core.Exceptions;
using Todo.Core.IRepository;
using Todo.Core.RequestOptions;
using Todo.Service.DataTransferObjects;
using Todo.Service.IService;

namespace Todo.Service.Service;
public class TodoService : ITodoService
{
    private readonly IRepositoryManager repository;
    private readonly IMapper mapper;

    public TodoService(IRepositoryManager repository , IMapper mapper)
    {
        this.repository = repository;
        this.mapper = mapper;
    }

    public async Task<IEnumerable<TodoDto>> GetAllTodoAsync(RequestParameter requestParamter)
    {
        var todoEntities = await repository.Todo.GelAllTodoAsync(requestParamter);
        var todoDtos = mapper.Map<IEnumerable<TodoDto>>(todoEntities);

        return todoDtos;
    }

    public async Task<Core.Entities.Todo> GetTodoEntityByIdAsync(Guid id , bool trackChanges)
    {
        var todoEntity = await GetTodoAndCheckIfItExistsAsync(id , trackChanges);

        return todoEntity;
    }

    public async Task<TodoDto> GetTodoByIdAsync(Guid id , bool trackChanges)
    {
        var todoEntity = await GetTodoAndCheckIfItExistsAsync(id , trackChanges);

        var todoDto = mapper.Map<TodoDto>(todoEntity);
        return todoDto;
    }

    public async Task<TodoDto> CreateTodoAsync(TodoForCreatinDto todoForCreatin)
    {
        var todoEntity = mapper.Map<Core.Entities.Todo>(todoForCreatin);

        repository.Todo.CreateTodo(todoEntity);
        await repository.SaveAsync();

        var todoDto = mapper.Map<TodoDto>(todoEntity);

        return todoDto;
    }

    public async Task UpdateTodoAsync(
        TodoForUpdatingDto todoForUpdating , Guid id , bool trackChanges
    )
    {
        var todoEntity = await GetTodoAndCheckIfItExistsAsync(id , trackChanges);

        mapper.Map(todoForUpdating , todoEntity);
        todoEntity.LastModifiedDate = DateOnly.FromDateTime(DateTime.Now);
        await repository.SaveAsync();
    }

    public async Task SaveChanges()
    {
        await repository.SaveAsync();
    }

    public async Task PartialUpdateDtoAsync(
        TodoDto dto , Guid id , bool trackChanges
    )
    {
        var todoEntity = await GetTodoAndCheckIfItExistsAsync(id , trackChanges);

        mapper.Map(dto , todoEntity);
        todoEntity.LastModifiedDate = DateOnly.FromDateTime(DateTime.Now);
        await repository.SaveAsync();
    }

    public async Task DeleteTodoAsync(Guid id , bool trackChanges)
    {
        var todoEntity = await GetTodoAndCheckIfItExistsAsync(id , trackChanges);

        repository.Todo.DeleteTodo(todoEntity);
        await repository.SaveAsync();
    }


    private async Task<Core.Entities.Todo> GetTodoAndCheckIfItExistsAsync(Guid id , bool trackChanges)
    {
        var todoEntity = await repository.Todo.GetTodoByIdAsync(id , trackChanges);
        if (todoEntity is null)
            throw new NotFoundException($"Todo With ID: {id} doesn't exist");

        return todoEntity;
    }
}
