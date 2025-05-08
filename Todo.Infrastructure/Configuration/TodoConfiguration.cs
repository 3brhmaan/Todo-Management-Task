using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Todo.Core.Enums;

namespace Todo.Infrastructure.Configuration;
public class TodoConfiguration : IEntityTypeConfiguration<Todo.Core.Entities.Todo>
{
    public void Configure(EntityTypeBuilder<Core.Entities.Todo> builder)
    {
        builder.Property(x => x.Status)
            .HasConversion(
                a => a.ToString() ,
                b => (TodoStatus)Enum.Parse(typeof(TodoStatus) , b)
            );

        builder.Property(x => x.Priority)
            .HasConversion(
                a => a.ToString() ,
                b => (TodoPriority)Enum.Parse(typeof(TodoPriority) , b)
            );
    }
}