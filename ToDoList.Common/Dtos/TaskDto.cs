using ToDoList.Data.Enums;

namespace ToDoList.Common.Dtos;

public class TaskDto
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string? Description { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime DueDate { get; set; }

    public Status Status { get; set; }

    public Guid UserId { get; set; }
}
