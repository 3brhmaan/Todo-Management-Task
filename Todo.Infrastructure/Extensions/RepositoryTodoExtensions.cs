using System.Linq.Dynamic.Core;

namespace Todo.Infrastructure.Extensions;
public static class RepositoryTodoExtensions
{
    public static IOrderedQueryable<Core.Entities.Todo> Sort(this IQueryable<Core.Entities.Todo> todos , string orderByQueryString)
    {
        if (string.IsNullOrWhiteSpace(orderByQueryString))
            return todos.OrderBy(e => e.Title);

        var orderQuery = OrderQueryBuilder.CreateOrderQuery<Core.Entities.Todo>(orderByQueryString);

        if (string.IsNullOrWhiteSpace(orderQuery))
            return todos.OrderBy(e => e.Title);

        return todos.OrderBy(orderQuery);
    }
}
