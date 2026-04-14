namespace VideoGameCatalog.Server.Import;

public class ImportResult
{
    public int RowsProcessed { get; set; }
    public int Inserted { get; set; }
    public int Updated { get; set; }
}