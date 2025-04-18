using Microsoft.AspNetCore.Components;
using System.Net;
using ToDoList.Common.Models;
using ToDoList.UI.Integrations;

namespace ToDoList.UI.PageCodeSource;

public class RegisterPageCodeSource : ComponentBase
{
    [Inject]
    public NavigationManager NavigationManager { get; set; } = default!;
    [Inject]
    public IUserIntegration UserIntegration { get; set; } = default!;
    public RegisterModel Model { get; set; } = new();
    public async Task Register()
    {
        var result = await UserIntegration.Register(Model);
        if (result == HttpStatusCode.OK)
        {
            NavigationManager.NavigateTo("/");
        }
    }
}
