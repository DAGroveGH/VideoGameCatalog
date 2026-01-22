//using Microsoft.AspNetCore.Components.Web;
//using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
//using VideoGameCatalog.Client;
//using VideoGameCatalog.Client.Services;

//var builder = WebAssemblyHostBuilder.CreateDefault(args);
//builder.RootComponents.Add<App>("#app");
//builder.RootComponents.Add<HeadOutlet>("head::after");

//builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

//builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
//builder.Services.AddHttpClient<IVideoGameApiService, VideoGameApiService>(
//    (sp, client) =>
//    {
//        var config = sp.GetRequiredService<IConfiguration>();
//        client.BaseAddress = new Uri(config["Api:BaseUrl"] ?? builder.HostEnvironment.BaseAddress);
//    });

//await builder.Build().RunAsync();


using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
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

await builder.Build().RunAsync();
