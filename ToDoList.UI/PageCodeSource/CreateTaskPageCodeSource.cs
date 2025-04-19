using Microsoft.AspNetCore.Components;
using System.Net;
using ToDoList.Common.Models;
using ToDoList.UI.Integrations;

namespace ToDoList.UI.PageCodeSource;

public class CreateTaskPageCodeSource : ComponentBase
{
    [Inject]
    public ITaskIntegration TaskIntegration { get; set; } = default!;
    [Inject]
    public NavigationManager NavigationManager { get; set; } = default!;
  
    public CreateTaskModel Model { get; set; } = new CreateTaskModel();

    public async Task CreateTask()
    {
        var response = await TaskIntegration.CreateTask(Model);
        if (response == HttpStatusCode.OK)
        {
            NavigationManager.NavigateTo("/");
        }
    }
}
