using CsvHelper;
using CsvHelper.Configuration;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using VideoGameCatalog.Server.Data;
using VideoGameCatalog.Server.Models;

namespace VideoGameCatalog.Server.Import;

public class CollectionCsvImportService
{
    private readonly ApplicationDbContext _db;

    public CollectionCsvImportService(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<ImportResult> Import(Stream csvStream)
    {
        using var reader = new StreamReader(csvStream);

        var config = new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            HeaderValidated = null,
            MissingFieldFound = null,
            BadDataFound = null,
            TrimOptions = TrimOptions.Trim,
            IgnoreBlankLines = true
        };

        using var csv = new CsvReader(reader, config);

        // handle placeholder tokens
        var intOptions = csv.Context.TypeConverterOptionsCache.GetOptions<int?>();
        intOptions.NullValues.AddRange(new[]
        {
            "",
            "?",
            "Missing Field",
            "-1"
        });

        var decimalOptions = csv.Context.TypeConverterOptionsCache.GetOptions<decimal?>();
        decimalOptions.NullValues.AddRange(new[]
        {
            "",
            "?",
            "Missing Field",
            "Digital",
            "-1",
            "-1.0"
        });

        var dateOptions = csv.Context.TypeConverterOptionsCache.GetOptions<DateTime?>();
        dateOptions.NullValues.AddRange(new[]
        {
            "",
            "?",
            "Missing Field"
        });

        var rows = csv.GetRecords<CollectionCsvRow>().ToList();

        var result = new ImportResult();

        // preload lookup tables
        var platforms = await _db.Platforms.ToDictionaryAsync(p => p.Name);
        var publishers = await _db.Publishers.ToDictionaryAsync(p => p.Name);
        var developers = await _db.Developers.ToDictionaryAsync(d => d.Name);
        var genres = await _db.Genres.ToDictionaryAsync(g => g.Name);

        // build fast lookup for existing collection items
        var existingItems = await _db.CollectionItems
            .Select(x => new
            {
                x.Id,
                x.Title,
                x.PlatformId,
                x.Ownership
            })
            .ToListAsync();

        var existingLookup = existingItems.ToDictionary(
            x => $"{x.Title}|{x.PlatformId}|{x.Ownership}",
            x => x.Id
        );

        foreach (var row in rows)
        {
            result.RowsProcessed++;

            var platform = await GetOrCreate(platforms, row.Platform, _db.Platforms);
            var publisher = await GetOrCreate(publishers, row.Publisher, _db.Publishers);
            var developer = await GetOrCreate(developers, row.Developer, _db.Developers);
            var genre = await GetOrCreate(genres, row.Genre, _db.Genres);

            var key = $"{row.Title}|{platform.Id}|{row.Ownership}";

            if (!existingLookup.TryGetValue(key, out var existingId))
            {
                var item = new CollectionItem
                {
                    Title = row.Title,
                    PlatformId = platform.Id,
                    Category = row.Category,
                    UserRecordType = row.UserRecordType,
                    Country = row.Country,
                    ReleaseType = row.ReleaseType,
                    PublisherId = publisher?.Id,
                    DeveloperId = developer?.Id,
                    GenreId = genre?.Id,
                    Ownership = row.Ownership,
                    PriceLoose = row.PriceLoose,
                    PriceCIB = row.PriceCIB,
                    PriceNew = row.PriceNew,
                    YourPrice = row.YourPrice,
                    PricePaid = row.PricePaid,
                    ItemCondition = row.ItemCondition,
                    BoxCondition = row.BoxCondition,
                    ManualCondition = row.ManualCondition,
                    Notes = row.Notes,
                    Tags = row.Tags,
                    Metacritic = row.Metacritic,
                    CreatedAt = row.CreatedAt.HasValue
                        ? DateTime.SpecifyKind(row.CreatedAt.Value, DateTimeKind.Utc)
                        : null
                };

                _db.CollectionItems.Add(item);
                result.Inserted++;
            }
            else
            {
                var existing = await _db.CollectionItems.FindAsync(existingId);

                if (existing == null)
                    continue;

                existing.Category = row.Category;
                existing.UserRecordType = row.UserRecordType;
                existing.Country = row.Country;
                existing.ReleaseType = row.ReleaseType;
                existing.PublisherId = publisher?.Id;
                existing.DeveloperId = developer?.Id;
                existing.GenreId = genre?.Id;
                existing.PriceLoose = row.PriceLoose;
                existing.PriceCIB = row.PriceCIB;
                existing.PriceNew = row.PriceNew;
                existing.YourPrice = row.YourPrice;
                existing.PricePaid = row.PricePaid;
                existing.ItemCondition = row.ItemCondition;
                existing.BoxCondition = row.BoxCondition;
                existing.ManualCondition = row.ManualCondition;
                existing.Notes = row.Notes;
                existing.Tags = row.Tags;
                existing.Metacritic = row.Metacritic;
                existing.CreatedAt = row.CreatedAt.HasValue
                    ? DateTime.SpecifyKind(row.CreatedAt.Value, DateTimeKind.Utc)
                    : null;

                result.Updated++;
            }
        }

        await _db.SaveChangesAsync();

        return result;
    }

    private async Task<T?> GetOrCreate<T>(
        Dictionary<string, T> cache,
        string name,
        DbSet<T> table) where T : class
    {
        if (string.IsNullOrWhiteSpace(name))
            return null;

        if (cache.TryGetValue(name, out var existing))
            return existing;

        var entity = (T)Activator.CreateInstance(typeof(T))!;
        typeof(T).GetProperty("Name")!.SetValue(entity, name);

        table.Add(entity);

        await _db.SaveChangesAsync();

        cache[name] = entity;

        return entity;
    }
}