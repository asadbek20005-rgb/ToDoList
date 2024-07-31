namespace DoList.Common.Models.Task
{
    public class UpdateTaskModel
    {
        public int Id { get; set; }
        public string Taskname { get; set; }
        public string Description { get; set; } 
        public string TaskType { get; set; }
        public DateOnly? DueDate { get; set; }
        public TimeOnly? DueTime { get; set; }
        public bool IsCompleted { get; set; }
    }
}
