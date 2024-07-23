using System.ComponentModel.DataAnnotations;

namespace DoList.Data.Entities
{
    public class Users
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public string Firstname { get; set; }
        [Required]
        public string Lastname { get; set; }
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }

        public DateTime CreatedAt { get; set; }
        public string? Role { get; set; }

        [Required]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        
        public virtual List<Tasks>? Tasks { get; set; }   
    }
}
