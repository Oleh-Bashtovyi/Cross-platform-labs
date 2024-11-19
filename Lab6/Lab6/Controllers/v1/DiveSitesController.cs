using Lab6.DTO;
using Lab6.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Lab6.Models;

namespace Lab6.Controllers.v1;

[Authorize]
[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
public class DiveSitesController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    public DiveSitesController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<DiveSiteResponse>>> GetDiveSites()
    {
        var diveSites = await _context.DiveSites
            .Select(ds => new DiveSiteResponse()
            {
                DiveSiteId = ds.DiveSiteId,
                DiveSiteCode = ds.DiveSiteCode,
                DiveSiteDescription = ds.DiveSiteDescription,
                DiveSiteName = ds.DiveSiteName,
                OtherDetails = ds.OtherDetails
            })
            .ToListAsync();

        return diveSites;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<DiveSiteResponse>> GetDiveSite(Guid id)
    {
        var site = await _context.DiveSites
            .Where(ds => ds.DiveSiteId == id)
            .Select(ds => new DiveSiteResponse()
            {
                DiveSiteId = ds.DiveSiteId,
                DiveSiteCode = ds.DiveSiteCode,
                DiveSiteDescription = ds.DiveSiteDescription,
                DiveSiteName = ds.DiveSiteName,
                OtherDetails = ds.OtherDetails
            })
            .FirstOrDefaultAsync();

        if (site == null)
        {
            return NotFound();
        }

        return site;
    }
}
