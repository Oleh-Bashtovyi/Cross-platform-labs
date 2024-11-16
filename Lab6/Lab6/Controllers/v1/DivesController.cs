using Azure.Core;
using Lab6.Data;
using Lab6.DTO;
using Lab6.Models;
using Microsoft.AspNetCore.Authorization;
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
    public async Task<ActionResult<IEnumerable<Dive>>> GetDives([FromQuery] DiveRequest request)
    {
        var query = FilterDives(_context.Dives.AsQueryable(), request);

        var result = await query.Select(d => new
        {
            d.DiveId,
            d.DiveDate,
            d.NightDiveYn,
            d.OtherDetails,
            d.DiverId,
            d.DiveSiteId,
            //Join
            d.Diver.DiverName,
            d.DiveSite.DiveSiteName
        }).ToListAsync();

        return Ok(result);
    }


    private IQueryable<Dive> FilterDives(IQueryable<Dive> query, DiveRequest request)
    {
        if (request.StartDate.HasValue)
            query = query.Where(d => d.DiveDate >= request.StartDate);

        if (request.EndDate.HasValue)
            query = query.Where(d => d.DiveDate <= request.EndDate);

        if (!string.IsNullOrEmpty(request.DiverId))
            query = query.Where(d => d.DiverId.ToString().StartsWith(request.DiverId));

        if (!string.IsNullOrEmpty(request.SiteNameStart))
            query = query.Where(d => d.DiveSite.DiveSiteName.ToLower().StartsWith(request.SiteNameStart.ToLower()));

        if (!string.IsNullOrEmpty(request.SiteNameEnd))
            query = query.Where(d => d.DiveSite.DiveSiteName.ToLower().EndsWith(request.SiteNameEnd.ToLower()));

        return query;
    }


    [HttpGet("{id}")]
    public async Task<ActionResult> GetDive(Guid id)
    {
        var dive = await _context.Dives
            .Where(d => d.DiveId == id)
            .Select(d => new
            {
                d.DiveId,
                d.DiveDate,
                d.NightDiveYn,
                d.OtherDetails,
                d.DiverId,
                d.DiveSiteId,
                //Join
                d.Diver.DiverName,
                d.DiveSite.DiveSiteName
            })
            .FirstOrDefaultAsync();

        if (dive == null)
        {
            return NotFound();
        }

        return Ok(dive);
    }


    [HttpPost]
    public async Task<ActionResult<Dive>> CreateDive(Dive dive)
    {
        _context.Dives.Add(dive);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetDive), new { id = dive.DiveId }, dive);
    }
}
