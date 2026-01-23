using System.Globalization;
using VideoGameCatalog.Server.Data.Dto;
using VideoGameCatalog.Shared.Models;

namespace VideoGameCatalog.Server.Repositories
{
    public class VideoGameCSVRepository
    {
        private readonly List<VideoGameModel> _games;

        public VideoGameCSVRepository(IWebHostEnvironment env)
        {
            var csvFilePath = Path.Combine(env.ContentRootPath, "Data", "2026_01_21_ge_collection.csv");
            if (!File.Exists(csvFilePath))
            {
                throw new FileNotFoundException("The CSV file was not found.", csvFilePath);
            }

            using var reader = new StreamReader(csvFilePath);

            var config = new CsvHelper.Configuration.CsvConfiguration(CultureInfo.InvariantCulture)
            {
                BadDataFound = null,
                MissingFieldFound = null,
                HeaderValidated = null,
                TrimOptions = CsvHelper.Configuration.TrimOptions.Trim
            };

            using var csv = new CsvHelper.CsvReader(reader, config);

            var records = csv.GetRecords<VideoGameCSVDTO>();
            _games = records.Select((r, index) => new VideoGameModel
            {
                Id = index + 1,
                Title = r.Title ?? "Unknown Title",
                Platform = r.Platform ?? "Unknown Platform",
                Category = r.Category ?? "Unknown Category",
                UserRecordType = r.UserRecordType ?? "Unknown Type",
                Country = r.Country ?? "Unknown Country",
                ReleaseType = r.ReleaseType ?? "Unknown Release Type",
                Publisher = r.Publisher ?? "Unknown Publisher",
                Developer = r.Developer ?? "Unknown Developer",
                Genre = r.Genre ?? "Unknown Genre",
                CreatedAt = r.CreatedAt ?? "Unknown Date",
                Ownership = r.Ownership ?? "Unknown Ownership",
                PriceLoose = r.PriceLoose ?? "Unknown Price",
                PriceCIB = r.PriceCIB ?? "Unknown Price",
                PriceNew = r.PriceNew ?? "Unknown Price",
                YourPrice = r.YourPrice ?? "Unknown Price",
                PricePaid = r.PricePaid ?? "Unknown Price",
                ItemCondition = r.ItemCondition ?? "Unknown Condition",
                BoxCondition = r.BoxCondition ?? "Unknown Box Condition",
                ManualCondition = r.ManualCondition ?? "Unknown Manual Condition",
                Notes = r.Notes ?? string.Empty,
                Tags = r.Tags ?? "No Tags",
                Metacritic = r.Metacritic ?? "Unknown Rating"
            }).ToList();
            
        }
        public IReadOnlyList<VideoGameModel> GetAllVideoGames() => _games;

        public VideoGameModel? GetVideoGameById(int id) => _games.FirstOrDefault(game => game.Id == id);
        
    }
}
