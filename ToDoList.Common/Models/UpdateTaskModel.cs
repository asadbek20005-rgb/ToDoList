using ToDoList.Data.Enums;

namespace ToDoList.Common.Models;

public class UpdateTaskModel
{
    public string? Title { get; set; }
    public string? Description { get; set; }
    public Status? Status { get; set; }
    public DateOnly? DueDate { get; set; }
}
