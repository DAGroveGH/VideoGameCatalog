using System;
using System.Collections.Generic;
using System.Text;

namespace VideoGameCatalog.Shared.Models
{
    public class VideoGameModel
    {
        public int Id { get; set; }
        public string? Platform { get; set; }
        public string? Category { get; set; }
        public string? UserRecordType { get; set; }
        public string? Title { get; set; }
        public string? Country { get; set; }
        public string? ReleaseType { get; set; }
        public string? Publisher { get; set; }
        public string? Developer { get; set; }
        public string? Genre { get; set; }
        public DateTime? CreatedAt { get; set; }
        public string? Ownership { get; set; }
        public double? PriceLoose { get; set; }
        public double? PriceCIB { get; set; }
        public double? PriceNew { get; set; }
        public double? YourPrice { get; set; }
        public double? PricePaid { get; set; }
        public string? ItemCondition { get; set; }
        public string? BoxCondition { get; set; }
        public string? ManualCondition { get; set; }
        public string? Notes { get; set; }
        public List<string>? tags { get; set; }
        public int? Metacritic { get; set; }
    }
}
