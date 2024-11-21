using Lab6.DTO;
using Lab6.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;

namespace Lab6.Controllers;

[Controller]
[Authorize]
public class DivesController : Controller
{
    private readonly Lab6ApiService _apiService;

    public DivesController(Lab6ApiService apiService)
    {
        _apiService = apiService;
    }

    [Route("/dives")]
    public async Task<IActionResult> Index([FromQuery]DiveRequest diveRequest)
    {
        try
        {
            var queryString = diveRequest.ToQueryString();

            var token = Request.Cookies["AccessToken"] ?? "";

            var divers = await _apiService.FetchData<List<DiveResponse>>(token, $"v1/dives/{queryString}");

            ViewData["StartDate"] = diveRequest.StartDate?.ToString("yyyy-MM-dd");
            ViewData["EndDate"] = diveRequest.EndDate?.ToString("yyyy-MM-dd");
            ViewData["DiverId"] = diveRequest.DiverId;
            ViewData["SiteNameStart"] = diveRequest.SiteNameStart;
            ViewData["SiteNameEnd"] = diveRequest.SiteNameEnd;

            return View(divers);
        }
        catch (HttpRequestException ex)
        {
            if (ex.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                return RedirectToAction("Login", "Account");
            }
            throw;
        }
        catch (Exception ex)
        {
            ModelState.AddModelError(string.Empty, $"Error: {ex.Message}");
            return View(diveRequest);
        }
    }

     
    [Route("/dives/{id:guid}")]
    public async Task<IActionResult> Details(Guid id)
    {
        try
        {
            var token = Request.Cookies["AccessToken"] ?? "";

            var diver = await _apiService.FetchData<DiveResponse>(token, $"v1/dives/{id}");

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
