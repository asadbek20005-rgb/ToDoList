using System.Net;
using System.Net.Http.Json;
using ToDoList.Common.Models;
using ToDoList.UI.Helpers;

namespace ToDoList.UI.Integrations;

public class TaskIntegration(HttpClient httpClient, ITokenHelper tokenHelper) : ITaskIntegration
{
    private readonly HttpClient _httpClient = httpClient;
    private readonly ITokenHelper _tokenHelper = tokenHelper;
    public async Task<HttpStatusCode> CreateTask(CreateTaskModel model)
    {
        await _tokenHelper.AddTokenToHeader();
        string url = $"/api/Users/userId/Tasks";
        var response = await _httpClient.PostAsJsonAsync(url, model);
        return response.StatusCode;
    }
}
