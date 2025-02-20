using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Frontend;
using Frontend.Services;
using ProductsApi;

WebAssemblyHostBuilder builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// Configures the HTTP client for the ProductsApiClient with the base address and default request headers.
// The HTTP client is managed by the dependency injection container and will be disposed of automatically.
// The IHttpClientFactory is used to create the client, which helps in managing the lifetime of the HttpClient instances
// by pooling and reusing them to reduce resource consumption and improve performance.
builder.Services.AddHttpClient<ProductsApiClient>(client =>
{
    client.BaseAddress = new Uri("http://localhost:5083");
    client.DefaultRequestHeaders.Add("Accept", "application/json");
});

builder.Services.AddSingleton<ChatService>();

await builder.Build().RunAsync();