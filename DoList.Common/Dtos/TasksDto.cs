namespace DoList.Common.Dtos
{
    public class TasksDto
    {
        public int Id { get; set; }
        public string Taskname { get; set; }
        public string Description { get; set; }
        public string TaskType { get; set; }
        public bool IsCompleted { get; set; }
          
    }
}
