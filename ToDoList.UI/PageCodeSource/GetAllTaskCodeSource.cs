using Microsoft.AspNetCore.Components;
using ToDoList.Common.Dtos;
using ToDoList.UI.Integrations;

namespace ToDoList.UI.PageCodeSource;

public class GetAllTaskCodeSource : ComponentBase
{
    [Inject]
    public ITaskIntegration TaskIntegration { get; set; } = default!;
    [Inject]
    public NavigationManager NavigationManager { get; set; } = default!;
    public List<TaskDto>? Tasks { get; set; }
    protected override async Task OnInitializedAsync()
    {
        var (status, tasks) = await TaskIntegration.GetAllTasks();
        if (status == System.Net.HttpStatusCode.OK)
        {
            Tasks = tasks;
        }
        else
        {
            NavigationManager.NavigateTo("/login");
        }
    }


    public void NavigateToTaskDetail(int taskId)
    {
        NavigationManager.NavigateTo($"/get-task/{taskId}");
    }
}
