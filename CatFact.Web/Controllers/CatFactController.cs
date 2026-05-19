using Microsoft.AspNetCore.Mvc;
using CatFact.Application.Interfaces;

namespace CatFact.Controllers;

public class CatFactController : Controller
{
    private readonly ICatFactService _catFactService;

    public CatFactController(ICatFactService catFactService)
    {
        _catFactService = catFactService;
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
}