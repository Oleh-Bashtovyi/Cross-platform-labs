using System.Diagnostics;
using Lab6.Services;
using Lab6.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Lab6.Controllers;

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
    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}