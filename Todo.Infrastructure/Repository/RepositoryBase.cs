using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Todo.Core.IRepository;
using Todo.Infrastructure.ApplicationContext;

namespace Todo.Infrastructure.Repository;
public class RepositoryBase<T> : IRepositoryBase<T> where T : class
{
    private readonly ApplicationDbContext dbContext;

    public RepositoryBase(ApplicationDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public IQueryable<T> FindAll(bool trackChanges = false)
    {
        return !trackChanges
            ? dbContext.Set<T>().AsNoTracking()
            : dbContext.Set<T>();
    }
    public IQueryable<T> FindByCondition(Expression<Func<T , bool>> expression , bool trackChanges)
    {
        return !trackChanges
            ? dbContext.Set<T>().Where(expression).AsNoTracking()
            : dbContext.Set<T>().Where(expression);
    }
    public void Create(T entity)
    {
        dbContext.Set<T>().Add(entity);
    }
    public void Delete(T entity)
    {
        dbContext.Set<T>().Remove(entity);
    }
    public void Update(T entity)
    {
        dbContext.Set<T>().Update(entity);
    }
}

