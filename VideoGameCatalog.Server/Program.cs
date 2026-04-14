using Microsoft.EntityFrameworkCore;
using VideoGameCatalog.Server.Data;
using VideoGameCatalog.Server.Import;
using VideoGameCatalog.Server.Mappings;
using VideoGameCatalog.Server.Repositories;
using VideoGameCatalog.Server.Services;

MapsterConfig.Register();

var builder = WebApplication.CreateBuilder(args);

//configuration
builder.Configuration
    .AddJsonFile("appsettings.json", optional: true)
    .AddUserSecrets<Program>(optional: true)
    .AddEnvironmentVariables();

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddControllers();
builder.Services.AddMemoryCache();
builder.Services.AddCors(options =>
{
    options.AddPolicy("ClientPolicy", policy =>
    {
        policy
            .WithOrigins("https://localhost:7249")
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials();
    });
});
builder.Services.AddScoped<IVideoGameRepository, VideoGameRepository>();

builder.Services.AddScoped<CollectionCsvImportService>();

builder.Services.AddHttpClient<IRawgExternalService, RawgExternalService>(client =>
{
    client.BaseAddress = new Uri("https://api.rawg.io/api/");
});

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseCors("ClientPolicy");
app.MapControllers();
app.MapFallbackToFile("index.html");
app.Run();


//app.MapGet("/ping", () => "pong");