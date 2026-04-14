using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Radzen;
using VideoGameCatalog.Client;
using VideoGameCatalog.Client.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// Load client config from wwwroot
builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

// Typed HttpClient for API access
builder.Services.AddHttpClient<IVideoGameApiService, VideoGameApiService>(
    (sp, client) =>
    {
        var config = sp.GetRequiredService<IConfiguration>();
        client.BaseAddress = new Uri(config["Api:BaseUrl"]!);
    });

builder.Services.AddHttpClient<VideoGameCSVApiService>(
    (sp, client) =>
    {
        var config = sp.GetRequiredService<IConfiguration>();
        client.BaseAddress = new Uri(config["Api:BaseUrl"]!);
    });

// HttpClient for image service
builder.Services.AddHttpClient<VideoGameImageService>(
    (sp, client) =>
    {
        var config = sp.GetRequiredService<IConfiguration>();
        client.BaseAddress = new Uri(config["Api:BaseUrl"]!);
    });

builder.Services.AddRadzenComponents();

await builder.Build().RunAsync();