using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;
using VideoGameCatalog.Server.Data;


namespace VideoGameCatalog.Server.Services;

public interface IRawgExternalService
{
    Task<Dictionary<string, string?>> GetGameImagesAsync(IEnumerable<string> titles);
}

public class RawgExternalService : IRawgExternalService
{
    private readonly HttpClient _httpClient;
    private readonly IConfiguration _config;
    private readonly ApplicationDbContext _db;

    public RawgExternalService(
        HttpClient httpClient,
        IConfiguration config,
        ApplicationDbContext db)
    {
        _httpClient = httpClient;
        _config = config;
        _db = db;
    }

    public async Task<Dictionary<string, string?>> GetGameImagesAsync(IEnumerable<string> titles)
    {
        var apiKey = _config["Rawg:ApiKey"];

        if (string.IsNullOrWhiteSpace(apiKey))
            throw new Exception("RAWG API key missing from configuration");

        var distinctTitles = titles.Distinct().ToList();

        var results = new Dictionary<string, string?>();

        // Load DB items once
        var dbItems = await _db.CollectionItems
            .Where(x => distinctTitles.Contains(x.Title.Trim()))
            .ToListAsync();

        var dbLookup = dbItems
            .GroupBy(x => x.Title)
            .ToDictionary(g => g.Key, g => g.First());

        foreach (var title in distinctTitles)
        {
            if (!dbLookup.TryGetValue(title, out var dbItem))
            {
                results[title] = null;
                continue;
            }

            // Already cached in DB
            if (!string.IsNullOrEmpty(dbItem.CoverImageUrl))
            {
                results[title] = dbItem.CoverImageUrl;
                continue;
            }

            try
            {
                var normalizedTitle = NormalizeTitle(title);

                var url =
                    $"games?key={apiKey}&search={Uri.EscapeDataString(normalizedTitle)}&page_size=1";

                var response =
                    await _httpClient.GetFromJsonAsync<RawgResponse>(url);

                var image =
                    response?.Results?.FirstOrDefault()?.BackgroundImage;

                results[title] = image;

                if (!string.IsNullOrEmpty(image))
                {
                    dbItem.CoverImageUrl = image;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"RAWG lookup failed for {title}: {ex.Message}");
                results[title] = null;
            }
        }

        await _db.SaveChangesAsync();

        return results;
    }

    private static string NormalizeTitle(string title)
    {
        if (string.IsNullOrWhiteSpace(title))
            return title;

        // remove bracketed edition info
        title = Regex.Replace(title, @"\[[^\]]*\]", "");

        // remove common edition names
        title = Regex.Replace(
            title,
            @"\b(Deluxe Edition|Ultimate Edition|Collector's Edition|Director's Cut|Gold Edition)\b",
            "",
            RegexOptions.IgnoreCase);

        return title.Trim();
    }

    private class RawgResponse
    {
        [JsonPropertyName("results")]
        public List<GameResult>? Results { get; set; }
    }

    private class GameResult
    {
        [JsonPropertyName("background_image")]
        public string? BackgroundImage { get; set; }
    }
}