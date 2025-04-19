using System.ComponentModel.DataAnnotations;
using ToDoList.Data.Enums;

namespace ToDoList.Data.Entites;

public class Task
{
    [Key]
    public int Id { get; set; }
    [Required]
    public string Title { get; set; }
    public string? Description { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    [Required]
    public DateOnly DueDate { get; set; }
    [Required]
    public Status Status { get; set; }

    public Guid UserId { get; set; }
}
