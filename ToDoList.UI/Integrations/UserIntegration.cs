using System.Net;
using System.Net.Http.Json;
using System.Threading.Tasks;
using ToDoList.Common.Models;

namespace ToDoList.UI.Integrations;

public class UserIntegration(HttpClient httpClient) : IUserIntegration
{
    private readonly HttpClient _httpClient = httpClient;
    public async Task<HttpStatusCode> Register(RegisterModel model)
    {
        const string url = "/api/Users/account/register";
        var response = await _httpClient.PostAsJsonAsync(url, model);
        return response.StatusCode;
    }
}
