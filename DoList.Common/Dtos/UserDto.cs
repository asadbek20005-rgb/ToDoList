using System.ComponentModel.DataAnnotations;

namespace DoList.Common.Dtos
{
    public class UserDto
    {
        public Guid Id { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Username { get; set; }
        public string Role { get; set; }
        public  List<TasksDto> Tasks { get; set; }
    }
}
