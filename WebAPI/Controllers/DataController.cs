using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using WebAPI.DTO;




namespace WebAPI.Controllers;

[ApiController]
[Route("api/pmv/[controller]")]
public class DataController : ControllerBase
{
    private readonly IWebHostEnvironment _webHostEnvironment;

    public DataController(IWebHostEnvironment webHostEnvironment)
    {
        _webHostEnvironment = webHostEnvironment;
    }

    [HttpGet("lookups")]
    public IActionResult GetJsonLookup() {

        string rootDirectory = _webHostEnvironment.ContentRootPath;

        string jsonData = System.IO.File.ReadAllText(Path.Combine(rootDirectory,"data/lookups.json"));
        
        // Deserialize the JSON data into an object
        var data = JsonSerializer.Deserialize<IEnumerable<LookupData>>(jsonData);

        // Return the serialized data as a string
        return Ok(data);
    }

   
}