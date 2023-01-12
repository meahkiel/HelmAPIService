using Applications.Interfaces;
using Core.PMV.Maintenance;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using WebAPI.DTO;




namespace WebAPI.Controllers;

[ApiController]
[Route("api/pmv/[controller]")]
public class DataController : ControllerBase
{
    private readonly IWebHostEnvironment _webHostEnvironment;
    private readonly IUnitWork _unitWork;

    public DataController(IWebHostEnvironment webHostEnvironment,IUnitWork unitWork)
    {
        _webHostEnvironment = webHostEnvironment;
        _unitWork = unitWork;
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

    [HttpGet("inspection")]
    public IActionResult GetInspectionJsonLookup() {

        string rootDirectory = _webHostEnvironment.ContentRootPath;

        string jsonData = System.IO.File.ReadAllText(Path.Combine(rootDirectory,"data/repair/inspections.json"));
        
        // Deserialize the JSON data into an object
        var data = JsonSerializer.Deserialize<IEnumerable<Inspection>>(jsonData);

        // Return the serialized data as a string
        return Ok(data);
    }

   
}