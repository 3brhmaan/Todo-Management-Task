namespace Todo.Core.IRepository;
public interface IRepositoryManager
{
    ITodoRepository Todo { get; }

    Task SaveAsync();
}

