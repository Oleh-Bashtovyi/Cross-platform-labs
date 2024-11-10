using Lab5.Services;
using Microsoft.AspNetCore.Mvc;

namespace Lab5.Controllers;

[Controller]
public class DivesController : Controller
{
    private readonly ApiService _apiService;

    public DivesController(ApiService apiService)
    {
        _apiService = apiService;
    }

    [Route("/dives")]
    public async Task<IActionResult> Index()
    {
        var divers = await _apiService.GetDivesAsync(apiVersion: "v2");

        return View(divers);
    }



    [Route("/dives/{id:guid}")]
    public async Task<IActionResult> Details(Guid id)
    {
        var diver = await _apiService.GetDiveAsync(id, apiVersion: "v2");

        if (diver == null)
        {
            return NotFound();
        }

        return View(diver);
    }
}
