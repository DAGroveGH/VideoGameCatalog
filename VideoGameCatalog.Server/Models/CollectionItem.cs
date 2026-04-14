namespace VideoGameCatalog.Server.Models;

public class CollectionItem
{
    public int Id { get; set; }
    public string Title { get; set; } = "";
    public int PlatformId { get; set; }
    public Platform Platform { get; set; } = null!;
    public string? Category { get; set; }
    public string? UserRecordType { get; set; }
    public string? Country { get; set; }
    public string? ReleaseType { get; set; }
    public int? PublisherId { get; set; }
    public Publisher? Publisher { get; set; }
    public int? DeveloperId { get; set; }
    public Developer? Developer { get; set; }
    public int? GenreId { get; set; }
    public Genre? Genre { get; set; }
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
    public string? CoverImageUrl { get; set; }

}