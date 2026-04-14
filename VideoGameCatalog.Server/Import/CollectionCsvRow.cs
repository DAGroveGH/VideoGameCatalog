namespace VideoGameCatalog.Server.Import;

public class CollectionCsvRow
{
    public string Platform { get; set; } = "";
    public string? Category { get; set; }
    public string? UserRecordType { get; set; }
    public string Title { get; set; } = "";
    public string? Country { get; set; }
    public string? ReleaseType { get; set; }
    public string? Publisher { get; set; }
    public string? Developer { get; set; }
    public string? Genre { get; set; }
    public DateTime? CreatedAt { get; set; }
    public string? Ownership { get; set; }
    public decimal? PriceLoose { get; set; }
    public decimal? PriceCIB { get; set; }
    public decimal? PriceNew { get; set; }
    public decimal? YourPrice { get; set; }
    public decimal? PricePaid { get; set; }
    public string? ItemCondition { get; set; }
    public string? BoxCondition { get; set; }
    public string? ManualCondition { get; set; }
    public string? Notes { get; set; }
    public string? Tags { get; set; }
    public int? Metacritic { get; set; }
}