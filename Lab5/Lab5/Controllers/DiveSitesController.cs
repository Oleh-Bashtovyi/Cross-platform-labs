using Lab6.DTO;
using Lab6.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Lab6.Controllers;

[Controller]
[Authorize]
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
        try
        {
            var token = Request.Cookies["AccessToken"] ?? "";

            var diveSites = await _apiService.GetData<List<DiveSiteResponse>>(token, "v1/dive-sites");

            return View(diveSites);
        }
        catch
        {
            return RedirectToAction("Login", "Account");
        }
    }


    [Route("/dive-sites/{id:guid}")]
    public async Task<IActionResult> Details(Guid id)
    {
        try
        {
            var token = Request.Cookies["AccessToken"] ?? "";

            var diveSite = await _apiService.GetData<DiveSiteResponse>(token, $"v1/dive-sites/{id}");

            if (diveSite == null)
            {
                return NotFound();
            }

            return View(diveSite);
        }
        catch
        {
            return RedirectToAction("Login", "Account");
        }
    }
}
