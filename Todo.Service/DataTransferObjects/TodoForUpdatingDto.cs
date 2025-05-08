using System.ComponentModel.DataAnnotations;
using Todo.Core.Attributes;
using Todo.Core.Enums;

namespace Todo.Service.DataTransferObjects;
public class TodoForCreatinDto
{
    [Required, StringLength(100)]
    public required string Title { get; set; }
    public string? Description { get; set; }
    [Range(0 , 2 , ErrorMessage = "Choose one of 3 options: 0 => Pending, 1 => In Progress, 2 => Completed")]
    public TodoStatus Status { get; set; }
    [Range(0 , 2 , ErrorMessage = "Choose one of 3 options: 0 => Low, 1 => Medium, 2 => High")]
    public TodoPriority Priority { get; set; }
    [FutureDate(ErrorMessage = "Invalid date format")]
    public DateOnly? DueDate { get; set; }
}
