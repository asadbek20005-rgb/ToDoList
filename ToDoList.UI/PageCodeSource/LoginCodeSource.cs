using Microsoft.AspNetCore.Components;
using System.Net;
using ToDoList.Common.Models;
using ToDoList.UI.Integrations;

namespace ToDoList.UI.PageCodeSource;

public class LoginCodeSource : ComponentBase
{
    [Inject]
    public IUserIntegration UserIntegration { get; set; } = default!;
    [Inject]
    public NavigationManager NavigationManager { get; set; } = default!;
    public LoginModel Model { get; set; } = new();
    public async Task Login()
    {
        var (statusCode, token) = await UserIntegration.Login(Model);
        if (statusCode == HttpStatusCode.OK)
        {
            NavigationManager.NavigateTo("/");
        }
    }
}
