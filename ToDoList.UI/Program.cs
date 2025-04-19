using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using ToDoList.UI;
using ToDoList.UI.Helpers;
using ToDoList.UI.Integrations;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7214") });
builder.Services.AddScoped<IUserIntegration, UserIntegration>();
builder.Services.AddScoped<ITaskIntegration, TaskIntegration>();
builder.Services.AddScoped<ITokenHelper, TokenHelper>();
builder.Services.AddBlazoredLocalStorage();
await builder.Build().RunAsync();
