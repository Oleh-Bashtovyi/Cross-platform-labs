using Lab6.Data;
using Lab6.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Lab6.Controllers.v2;

//[Authorize]
[ApiController]
[ApiVersion("2.0")]
[Produces("application/json")]
[Route("api/v{version:apiVersion}/[controller]")]
public class DivesController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    public DivesController(ApplicationDbContext context)
    {
        _context = context;
    }

    //[HttpGet]
    //[Produces("application/json")]
    //public async Task<ActionResult<IEnumerable<Dive>>> GetDives()
    //{
    //    var dives = await _context.Dives
    //        .Include(d => d.Diver)
    //        .Include(d => d.DiveSite)
    //        .ToListAsync();

    //    return dives;
    //}



    //[HttpGet]
    //[Produces("application/json")]
    //public async Task<ActionResult<IEnumerable<Dive>>> GetDives()
    //{
    //    var dives = await _context.Dives
    //        .Include(d => d.Diver)
    //        .Include(d => d.DiveSite)
    //        .ToListAsync();

    //    return dives;
    //}



    [HttpGet]
    [Produces("application/json")]
    public async Task<ActionResult<IEnumerable<Dive>>> GetDives()
    {
        var dives = await _context.Dives
            .Include(d => d.Diver)
            .Include(d => d.DiveSite)
            .ToListAsync();

        return Ok(dives);
    }


    [HttpGet("{id}")]
    public async Task<ActionResult<Dive>> GetDive(Guid id)
    {
        var dive = await _context.Dives
            .Include(d => d.Diver)
            .Include(d => d.DiveSite)
            .FirstOrDefaultAsync(d => d.DiveId == id);

        if (dive == null)
        {
            return NotFound();
        }

        return dive;
    }
}