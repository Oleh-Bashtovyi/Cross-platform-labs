using Lab6.DTO;
using Lab6.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Lab6.Models;

namespace Lab6.Controllers.v2;

[Authorize]
[ApiController]
[ApiVersion("2.0")]
[Route("api/v{version:apiVersion}/dive-sites")]
public class DiveSitesController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    public DiveSitesController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<DiveSiteResponseV2>>> GetDiveSites()
    {
        var diveSites = await _context.DiveSites
            .Include(ds => ds.Wreck)
            .Include(ds => ds.DiveSiteType)
            .Select(ds => new DiveSiteResponseV2()
            {
                DiveSiteId = ds.DiveSiteId,
                DiveSiteCode = ds.DiveSiteCode,
                DiveSiteDescription = ds.DiveSiteDescription,
                DiveSiteName = ds.DiveSiteName,
                OtherDetails = ds.OtherDetails,
                //Addition fields from join
                WreckDate = ds.Wreck.WreckDate,
                DiveSiteTypeDetails = ds.DiveSiteType.DiveSiteDetails
            })
            .ToListAsync();

        return Ok(diveSites);
    }


    [HttpGet("{id}")]
    public async Task<ActionResult<DiveSiteResponseV2>> GetDiveSite(Guid id)
    {
        var site = await _context.DiveSites
            .Include(ds => ds.Wreck)
            .Include(ds => ds.DiveSiteType)
            .Where(ds => ds.DiveSiteId == id)
            .Select(ds => new DiveSiteResponseV2()
            {
                DiveSiteId = ds.DiveSiteId,
                DiveSiteCode = ds.DiveSiteCode,
                DiveSiteDescription = ds.DiveSiteDescription,
                DiveSiteName = ds.DiveSiteName,
                OtherDetails = ds.OtherDetails,
                //Addition fields from join
                WreckDate = ds.Wreck.WreckDate,
                DiveSiteTypeDetails = ds.DiveSiteType.DiveSiteDetails
            })
            .FirstOrDefaultAsync();

        if (site == null)
        {
            return NotFound();
        }

        return site;
    }
}
