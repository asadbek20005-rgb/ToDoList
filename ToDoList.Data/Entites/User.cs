using System.ComponentModel.DataAnnotations;

namespace ToDoList.Data.Entites;

public class User
{
    [Key]
    public Guid Id { get; set; }
    [Required]
    public string Username { get; set; }
    [Required]
    public string PasswordHash { get; set; }

    public List<Task>? Tasks { get; set; }
}