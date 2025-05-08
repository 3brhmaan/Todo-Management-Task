using System.ComponentModel.DataAnnotations;

namespace Todo.Service.DataTransferObjects;
public class TodoDto
{
    public Guid Id { get; set; }
    [Required, StringLength(100)]
    public required string Title { get; set; }
    public string? Description { get; set; }
    public string Status { get; set; }
    public string Priority { get; set; }
    public DateOnly? DueDate { get; set; }
}
