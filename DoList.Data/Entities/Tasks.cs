using System.ComponentModel.DataAnnotations;

namespace DoList.Data.Entities
{
    public class Tasks
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Taskname {  get; set; }
        public string? Description { get; set; }
        public string? TaskType { get; set; }
        public DateOnly DueDate { get; set; }
        public TimeOnly DueTime { get; set; }
        public bool IsCompleted { get; set; }   

        public Guid UserId { get; set; }
        public virtual Users? User { get; set; }
    }
}
