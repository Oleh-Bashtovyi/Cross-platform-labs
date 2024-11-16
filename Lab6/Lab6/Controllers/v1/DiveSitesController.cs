using Lab6.Data;
using Lab6.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
    public async Task<ActionResult<IEnumerable<DiveSite>>> GetDiveSites()
    {
        return await _context.DiveSites.ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<DiveSite>> GetDiveSite(Guid id)
    {
        var site = await _context.DiveSites.FindAsync(id);

        if (site == null)
        {
            return NotFound();
        }

        return site;
    }

    [HttpPost]
    public async Task<ActionResult<DiveSite>> CreateDiveSite(DiveSite site)
    {
        _context.DiveSites.Add(site);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetDiveSite), new { id = site.DiveSiteId }, site);
    }
}
