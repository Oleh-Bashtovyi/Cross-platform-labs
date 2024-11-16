using Lab6.Data;
using Lab6.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Lab6.Controllers.v1;

[Authorize]
[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
public class DiveOrganisationsController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    public DiveOrganisationsController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<DiveOrganisation>>> GetDiveOrganisations()
    {
        return await _context.DiveOrganisations.ToListAsync();
    }

    [HttpGet("{code}")]
    public async Task<ActionResult<DiveOrganisation>> GetDiveOrganisation(string code)
    {
        var org = await _context.DiveOrganisations.FindAsync(code);

        if (org == null)
        {
            return NotFound();
        }
        return org;
    }

    [HttpPost]
    public async Task<ActionResult<DiveOrganisation>> CreateDiveOrganisation(DiveOrganisation org)
    {
        _context.DiveOrganisations.Add(org);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetDiveOrganisation), new { code = org.OrganisationCode }, org);
    }
}
