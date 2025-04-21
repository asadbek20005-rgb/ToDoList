using Microsoft.AspNetCore.Components;
using System.Net;
using ToDoList.Common.Dtos;
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


    public void SaveTask()
    {

    }
}
