using Lab6.Data;
using Lab6.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Lab6.Controllers.v1;

[Authorize]
[ApiController]
[ApiVersion("1.0")]
[Produces("application/json")]
[Route("api/v{version:apiVersion}/[controller]")]
public class DiversController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    public DiversController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Diver>>> GetDivers()
    {
        return await _context.Divers.ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Diver>> GetDiver(Guid id)
    {
        var diver = await _context.Divers.FindAsync(id);

        if (diver == null)
        {
            return NotFound();
        }

        return diver;
    }

    [HttpPost]
    public async Task<ActionResult<Diver>> CreateDiver(Diver diver)
    {
        _context.Divers.Add(diver);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetDiver), new { id = diver.DiverId }, diver);
    }
}
