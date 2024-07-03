using System.Threading.Tasks;
using DoList.Common.Dtos;
using DoList.Data.Entities;
using Mapster;

namespace DoList.Service.ConvertToExtension
{
    public static class Convert
    {
        public static UserDto ParseToModel(this Users user) { return user.Adapt<UserDto>(); }
        public static TasksDto ParseToModel(this Tasks tasks) { return tasks.Adapt<TasksDto>();}

        public static List<UserDto> ParseToModels(this List<Users> users)
        {
            if (users is null || users.Count == 0) return new List<UserDto>();
            var model = new List<UserDto>();
            users.ForEach(user => { model.Add(user.ParseToModel()); });
            return model;
        }


        public static List<TasksDto> ParseToModels(this List<Tasks> tasks)
        {
            if(tasks is null || tasks.Count == 0) return new List<TasksDto>(); 
            var model = new List<TasksDto>();
            tasks.ForEach(task => { model.Add(task.ParseToModel()); });
            return model;
        } 
        
    }
}
