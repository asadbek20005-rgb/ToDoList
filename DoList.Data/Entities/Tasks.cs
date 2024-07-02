namespace DoList.Data.Entities
{
    public class Tasks
    {
        public int Id { get; set; }
        public string Taskname {  get; set; }
        public string Description { get; set; }
        public string TaskType { get; set; }
        public bool IsCompleted { get; set; }   

        public Guid UserId { get; set; }
        public Users User { get; set; }
    }
}
