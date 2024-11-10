using System.Diagnostics;
using Lab5.Services;
using Lab5.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Lab5.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly ApiService _service;

    public HomeController(ILogger<HomeController> logger, ApiService service)
    {
        _service = service;
        _logger = logger;
    }



    [Route("/")]
    public IActionResult Index()
    {
        return View();
    }

    [Route("/privacy")]
    public async Task<IActionResult> Privacy()
    {
        var data = await _service.GetDiversAsync();

        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}