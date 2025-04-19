using Blazored.LocalStorage;
using System.Net.Http.Headers;

namespace ToDoList.UI.Helpers;
public class TokenHelper(HttpClient httpClient, ILocalStorageService localStorageService) : ITokenHelper
{
    private const string Key = "token";
    private readonly HttpClient _httpClient = httpClient;
    private readonly ILocalStorageService _localStorageService = localStorageService;

    public async Task AddTokenToHeader()
    {
        var token = await _localStorageService.GetItemAsStringAsync(key: Key);

        if (!string.IsNullOrEmpty(token))
        {
            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("video/mp4"));

        }

    }


}
