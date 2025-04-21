using Microsoft.AspNetCore.Components;
using System.Net;
using ToDoList.Common.Dtos;
using ToDoList.Common.Models;
using ToDoList.UI.Integrations;

namespace ToDoList.UI.PageCodeSource;

public class GetTaskPageCodeSource : ComponentBase
{
    [Inject]
    public ITaskIntegration TaskIntegration { get; set; } = default!;
    [Inject]
    public NavigationManager NavigationManager { get; set; } = default!;
    public TaskDto TaskDto { get; set; } = new();
    [Parameter]
    public int TaskId { get; set; }
    protected override async Task OnInitializedAsync()
    {
        var (statusCode, task) = await TaskIntegration.GetTaskById(TaskId);
        if (statusCode == HttpStatusCode.OK)
        {
            if (task is not null)
                TaskDto = task;
        }
    }


    public async Task SaveTask()
    {
        var updatedTask = new UpdateTaskModel
        {
            Title = TaskDto.Title,
            Description = TaskDto.Description,
            Status = TaskDto.Status,
            DueDate = TaskDto.DueDate
        };
        var statusCode = await TaskIntegration.UpdateTask(TaskId, updatedTask);
        if (statusCode == HttpStatusCode.OK)
        {
            NavigationManager.NavigateTo("/all-tasks");
        }
    }


    public async Task DeleteTask()
    {
        var statusCode = await TaskIntegration.DeleteTask(TaskId);
        if (statusCode == HttpStatusCode.OK)
        {
            NavigationManager.NavigateTo("/all-tasks");
        }
    }

    public void NavigateToAllTasks()
    {
        NavigationManager.NavigateTo("/all-tasks");
    }

}
