using System.ComponentModel.DataAnnotations;

namespace ToDoList.Common.Models;

public class RegisterModel
{
    [Required]
    public string Username { get; set; }
    [Required]
    public string Password { get; set; }

    [Required]
    [Compare(nameof(Password), ErrorMessage = "Password and confirmation password do not match.")]
    public string ConfirmPassword { get; set; }
}
