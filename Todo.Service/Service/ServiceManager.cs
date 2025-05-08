using AutoMapper;
using Todo.Core.IRepository;
using Todo.Service.IService;

namespace Todo.Service.Service;
public class ServiceManager : IServiceManager
{
    private readonly Lazy<ITodoService> todo;

    public ServiceManager(
        IRepositoryManager repositoryManager , IMapper mapper
    )
    {
        todo = new Lazy<ITodoService>(() => new TodoService(repositoryManager , mapper));
    }


    public ITodoService Todo => todo.Value;
}
