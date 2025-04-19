using Blazored.LocalStorage;
using System.Net;
using System.Net.Http.Json;
using ToDoList.Common.Models;

namespace ToDoList.UI.Integrations;

public class UserIntegration(HttpClient httpClient, ILocalStorageService localStorageService) : IUserIntegration
{
    private readonly HttpClient _httpClient = httpClient;
    private readonly ILocalStorageService _localStorageService = localStorageService;
    public async Task<Tuple<HttpStatusCode, string>> Login(LoginModel model)
    {
        string url = "/api/Users/account/login";
        var response = await _httpClient.PostAsJsonAsync(url, model);
        var token = await response.Content.ReadAsStringAsync();
        await _localStorageService.SetItemAsStringAsync("token", token);
        return new(response.StatusCode, token);
    }

    public async Task<HttpStatusCode> Register(RegisterModel model)
    {
        const string url = "/api/Users/account/register";
        var response = await _httpClient.PostAsJsonAsync(url, model);
        return response.StatusCode;
    }
}
