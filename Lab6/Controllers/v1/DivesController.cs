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
public class DivesController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    public DivesController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Dive>>> GetDives()
    {
        return await _context.Dives.ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Dive>> GetDive(Guid id)
    {
        var dive = await _context.Dives.FindAsync(id);

        if (dive == null)
        {
            return NotFound();
        }

        return dive;
    }

    [HttpPost]
    public async Task<ActionResult<Dive>> CreateDive(Dive dive)
    {
        _context.Dives.Add(dive);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetDive), new { id = dive.DiveId }, dive);
    }
}
