using Lab5.DTO;
using Lab5.Services;
using Microsoft.AspNetCore.Mvc;

namespace Lab5.Controllers;

[Controller]
public class DiversController : Controller
{
    private readonly ApiService _apiService;

    public DiversController(ApiService apiService)
    {
        _apiService = apiService;
    }

    [Route("/divers")]
    public async Task<IActionResult> Index()
    {
        var divers = await _apiService.GetDiversAsync();

        return View(divers);
    }



    [Route("/divers/{id:guid}")]
    public async Task<IActionResult> Details(Guid id)
    {
        var diver = await _apiService.GetDiverAsync(id);

        if (diver == null)
        {
            return NotFound();
        }

        return View(diver);
    }
}
