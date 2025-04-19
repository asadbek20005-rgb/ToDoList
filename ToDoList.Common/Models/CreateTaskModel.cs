using System.ComponentModel.DataAnnotations;
using ToDoList.Data.Enums;

namespace ToDoList.Common.Models;

public class CreateTaskModel
{
    [Required]
    public string Title { get; set; }
    public string? Description { get; set; }
    [Required]
    public Status Status { get; set; }
    public DateOnly DueDate { get; set; }
}
