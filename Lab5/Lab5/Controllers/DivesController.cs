using Lab5.DTO;
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
    public async Task<IActionResult> Index([FromQuery]DiveRequest diveRequest)
    {
        var divers = await _apiService.GetDivesAsync(diveRequest, apiVersion: "v1");

        ViewData["StartDate"] = diveRequest.StartDate;
        ViewData["EndDate"] = diveRequest.EndDate;
        ViewData["DiverId"] = diveRequest.DiverId;
        ViewData["SiteNameStart"] = diveRequest.SiteNameStart;
        ViewData["SiteNameEnd"] = diveRequest.SiteNameEnd;

        return View(divers);
    }



    [Route("/dives/{id:guid}")]
    public async Task<IActionResult> Details(Guid id)
    {
        var diver = await _apiService.GetDiveAsync(id, apiVersion: "v1");

        if (diver == null)
        {
            return NotFound();
        }

        return View(diver);
    }
}
