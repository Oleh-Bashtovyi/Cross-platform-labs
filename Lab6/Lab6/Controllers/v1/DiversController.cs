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
    public async Task<ActionResult<IEnumerable<DiverResponse>>> GetDivers()
    {
        var divers = await _context.Divers.Select(d => new DiverResponse()
        {
            DiverId = d.DiverId,
            DiverName = d.DiverName,
            DiverDetails = d.DiverDetails,
        }).ToListAsync();

        return divers;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<DiverResponse>> GetDiver(Guid id)
    {
        var diver = await _context.Divers
            .Where(d => d.DiverId == id)
            .Select(d => new DiverResponse()
            {
                DiverId = d.DiverId,
                DiverName = d.DiverName,
                DiverDetails = d.DiverDetails,
            })
            .FirstOrDefaultAsync();

        if (diver == null)
        {
            return NotFound();
        }

        return diver;
    }
}
