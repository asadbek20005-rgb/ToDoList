using System.Net;
using System.Net.Http.Json;
using ToDoList.Common.Models;

namespace ToDoList.UI.Integrations;

public class UserIntegration(HttpClient httpClient) : IUserIntegration
{
    private readonly HttpClient _httpClient = httpClient;

    public async Task<Tuple<HttpStatusCode, string>> Login(LoginModel model)
    {
        string url = "/api/Users/account/login";
        var response = await _httpClient.PostAsJsonAsync(url, model);
        return new(response.StatusCode, await response.Content.ReadAsStringAsync());
    }

    public async Task<HttpStatusCode> Register(RegisterModel model)
    {
        const string url = "/api/Users/account/register";
        var response = await _httpClient.PostAsJsonAsync(url, model);
        return response.StatusCode;
    }
}
