using Lab6.DTO;
using Lab6.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Lab6.Controllers;

[Controller]
[Authorize]
public class DiveSitesController : Controller
{
    private readonly Lab6ApiService _apiService;

    public DiveSitesController(Lab6ApiService apiService)
    {
        _apiService = apiService;
    }

    [Route("/dive-sites-v1")]
    public async Task<IActionResult> IndexV1()
    {
        try
        {
            var token = Request.Cookies["AccessToken"] ?? "";

            var diveSites = await _apiService.FetchData<List<DiveSiteResponse>>(token, "v1/dive-sites");

            return View(diveSites);
        }
        catch
        {
            return RedirectToAction("Login", "Account");
        }
    }


    [Route("/dive-sites-v2")]
    public async Task<IActionResult> IndexV2()
    {
        try
        {
            var token = Request.Cookies["AccessToken"] ?? "";

            var diveSites = await _apiService.FetchData<List<DiveSiteResponseV2>>(token, "v2/dive-sites");

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

            var diveSite = await _apiService.FetchData<DiveSiteResponse>(token, $"v1/dive-sites/{id}");

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
