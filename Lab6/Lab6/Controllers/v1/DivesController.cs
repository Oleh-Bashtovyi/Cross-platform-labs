using Lab6.Data;
using Lab6.DTO;
using Lab6.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Lab6.Controllers.v1;

[Authorize]
[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
public class DivesController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    public DivesController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<DiveResponse>>> GetDives([FromQuery] DiveRequest request)
    {
        Console.WriteLine(HttpContext.Request.GetDisplayUrl());

        var query = _context.Dives
            .Include(d => d.Diver)
            .Include(d => d.DiveSite)
            .AsQueryable();

        if (request.StartDate.HasValue)
        {
            var fromDate = DateTime.SpecifyKind(request.StartDate.Value, DateTimeKind.Utc);
            var startOfDay = fromDate.Date;
            query = query.Where(d => d.DiveDate >= startOfDay);
        }

        if (request.EndDate.HasValue)
        {
            var toDate = DateTime.SpecifyKind(request.EndDate.Value, DateTimeKind.Utc);
            var endOfDay = toDate.Date.AddDays(1).AddMilliseconds(-1);
            query = query.Where(d => d.DiveDate <= endOfDay);
        }

        if (request.DiverId.HasValue)
        {
            query = query.Where(d => d.DiverId == request.DiverId);
        }

        if (!string.IsNullOrEmpty(request.SiteNameStart))
        {
            var siteNameStart = request.SiteNameStart.ToLower();
            query = query.Where(d => d.DiveSite.DiveSiteName.ToLower().StartsWith(siteNameStart));
        }

        if (!string.IsNullOrEmpty(request.SiteNameEnd))
        {
            var siteNameEnd = request.SiteNameEnd.ToLower();
            query = query.Where(d => d.DiveSite.DiveSiteName.ToLower().EndsWith(siteNameEnd));
        }

        var ukraineTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Europe/Kiev");

        var dives = await query.Select(d => new DiveResponse()
        {
            DiveId = d.DiveId,
            DiveDate = TimeZoneInfo.ConvertTimeFromUtc(d.DiveDate, ukraineTimeZone),
            NightDiveYn = d.NightDiveYn,
            OtherDetails = d.OtherDetails,
            DiverId = d.DiverId,
            DiveSiteId = d.DiveSiteId,
            //Join
            DiverName = d.Diver.DiverName,
            DiveSiteName = d.DiveSite.DiveSiteName,
        })
        .OrderBy(d => d.DiverId)
        .ToListAsync();

        return dives;
    }

    

    [HttpGet("{id}")]
    public async Task<ActionResult<DiveResponse>> GetDive(Guid id)
    {
        var dive = await _context.Dives
            .Include(d => d.Diver)
            .Include(d => d.DiveSite)
            .Where(d => d.DiveId == id)
            .Select(d => new DiveResponse()
            {
                DiveId = d.DiveId,
                DiveDate = d.DiveDate,
                NightDiveYn = d.NightDiveYn,
                OtherDetails = d.OtherDetails,
                DiverId = d.DiverId,
                DiveSiteId = d.DiveSiteId,
                //Join
                DiverName = d.Diver.DiverName,
                DiveSiteName = d.DiveSite.DiveSiteName,
            })
            .FirstOrDefaultAsync();

        if (dive == null)
        {
            return NotFound();
        }

        var ukraineTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Europe/Kiev");
        dive.DiveDate = TimeZoneInfo.ConvertTimeFromUtc(dive.DiveDate, ukraineTimeZone);

        return dive;
    }
}
