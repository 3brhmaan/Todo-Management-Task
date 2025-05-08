using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Todo.Core.Enums;

namespace Todo.Core.Entities;
public class Todo
{
    public Guid Id { get; set; }
    [Required, StringLength(100), Column(TypeName = "nvarchar(100)")]
    public string Title { get; set; }
    public string? Description { get; set; }
    public TodoStatus Status { get; set; } = TodoStatus.Pending;
    public TodoPriority Priority { get; set; } = TodoPriority.Low;
    public DateOnly? DueDate { get; set; }
    public DateOnly CreatedDate { get; set; } = DateOnly.FromDateTime(DateTime.Now);
    public DateOnly LastModifiedDate { get; set; } = DateOnly.FromDateTime(DateTime.Now);
}