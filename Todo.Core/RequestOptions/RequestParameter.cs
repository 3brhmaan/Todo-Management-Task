using System.ComponentModel.DataAnnotations;
using Todo.Core.Enums;

namespace Todo.Core.RequestOptions;
public class RequestParameter
{
    [Range(0 , 3 , ErrorMessage = "Choose one of 4 options: 0 => Pending, 1 => In Progress, 2 => Completed, 3 => All")]
    public TodoStatus Status { get; set; } = TodoStatus.All;
    [Range(0 , 3 , ErrorMessage = "Choose one of 4 options: 0 => Low, 1 => Medium, 2 => High, 3 => All")]
    public TodoPriority Priority { get; set; } = TodoPriority.All;
    public DateOnly StartDate { get; set; } = default;
    public DateOnly EndDate { get; set; } = default;
    public string? OrderBy { get; set; } = "LastModifiedDate asc";
}