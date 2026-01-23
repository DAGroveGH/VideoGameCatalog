using CsvHelper;
using CsvHelper.Configuration;
using System.Globalization;
using VideoGameCatalog.Server.Data.Dto;

namespace VideoGameCatalog.Server.Services
{
    public class VideoGameCsvService
    {
        private const string CsvPath = "Data/videogames.csv";

        public IReadOnlyList<VideoGameCSVDTO> LoadAll()
        {
            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                BadDataFound = null,
                MissingFieldFound = null,
                HeaderValidated = null,
                TrimOptions = TrimOptions.Trim
            };

            using var reader = new StreamReader(CsvPath);
            using var csv = new CsvReader(reader, config);

            return csv.GetRecords<VideoGameCSVDTO>().ToList();
        }
    }
}
