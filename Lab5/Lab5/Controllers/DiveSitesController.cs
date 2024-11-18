using Lab6.Services;
using Microsoft.AspNetCore.Mvc;

namespace Lab6.Controllers;

[Controller]
public class DiveSitesController : Controller
{
    private readonly ApiService _apiService;

    public DiveSitesController(ApiService apiService)
    {
        _apiService = apiService;
    }

    [Route("/dive-sites")]
    public async Task<IActionResult> Index()
    {
        var divers = await _apiService.GetDiveSitesAsync();

        return View(divers);
    }


    [Route("/dive-sites/{id:guid}")]
    public async Task<IActionResult> Details(Guid id)
    {
        var diver = await _apiService.GetDiveSiteAsync(id);

        if (diver == null)
        {
            return NotFound();
        }

        return View(diver);
    }
}
