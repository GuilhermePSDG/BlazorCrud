using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Web;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");
builder.Services.AddScoped<ItemsClient>();

var apiUrl = "https://localhost:7208";
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(apiUrl) });
var app = builder.Build();
await app.RunAsync();
