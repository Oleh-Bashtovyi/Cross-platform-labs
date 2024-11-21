using Lab6.DTO;
using Lab6.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Lab6.Controllers;

[Authorize]
[Controller]
public class DiversController : Controller
{
    private readonly Lab6ApiService _apiService;

    public DiversController(Lab6ApiService apiService)
    {
        _apiService = apiService;
    }

    [Route("/divers")]
    public async Task<IActionResult> Index()
    {
        try
        {
            var token = Request.Cookies["AccessToken"] ?? "";

            var divers = await _apiService.FetchData<List<DiverResponse>>(token, "v1/divers");

            return View(divers);
        }
        catch
        {
            return RedirectToAction("Login", "Account");
        }
    }



    [Route("/divers/{id:guid}")]
    public async Task<IActionResult> Details(Guid id)
    {
        try
        {
            var token = Request.Cookies["AccessToken"] ?? "";

            var diver = await _apiService.FetchData<DiverResponse>(token, $"v1/divers/{id}");

            if (diver == null)
            {
                return NotFound();
            }

            return View(diver);
        }
        catch
        {
            return RedirectToAction("Login", "Account");
        }
    }
}
