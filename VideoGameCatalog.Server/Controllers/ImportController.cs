using Microsoft.AspNetCore.Mvc;
using VideoGameCatalog.Server.Import;

namespace VideoGameCatalog.Server.Controllers;

[ApiController]
[Route("api/import")]
public class ImportController : ControllerBase
{
    private readonly CollectionCsvImportService _importService;

    public ImportController(CollectionCsvImportService importService)
    {
        _importService = importService;
    }

    [HttpPost("csv")]
    public async Task<IActionResult> Upload()
    {
        try
        {
            var file = Request.Form.Files.FirstOrDefault();

            if (file == null)
                return BadRequest("No file uploaded");

            using var stream = file.OpenReadStream();

            var result = await _importService.Import(stream);

            return Ok(result);
        }
        catch (Exception ex)
        {
            return Problem(ex.ToString());
        }
    }
}