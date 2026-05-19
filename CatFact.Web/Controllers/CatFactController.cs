using Microsoft.AspNetCore.Mvc;
using CatFact.Application.Interfaces;

namespace CatFact.Controllers;

public class CatFactController : Controller
{
    private readonly ICatFactService _catFactService;
    private readonly IExportService _catFactExportService;

    public CatFactController(
        ICatFactService catFactService,
        IExportService catFactExportService)
    {
        _catFactService = catFactService;
        _catFactExportService = catFactExportService;
    }

    [HttpGet]
    public IActionResult Index()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Generate()
    {
        var fact = await _catFactService.GenerateFactAsync();
        if (fact == null)
        {
            return View("Error");
        }
        return View("Index", fact);
    }
    [HttpPost]
    public async Task<IActionResult> ExportJson()
    {
        var json = await _catFactExportService.GetAllAsJsonAsync();
        var fileName = $"catfacts_{DateTime.Now:yyyyMMdd_HHmmss}.json";
        var bytes = System.Text.Encoding.UTF8.GetBytes(json);
        return File(bytes, "application/json", fileName);
    }
}