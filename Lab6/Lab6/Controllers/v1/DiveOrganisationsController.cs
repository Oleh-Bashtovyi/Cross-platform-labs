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
        var diveOrg = await _context.DiveOrganisations.ToListAsync();

        return diveOrg;
    }

    [HttpGet("{code}")]
    public async Task<IActionResult> GetDiveOrganisation(string code)
    {
        var org = await _context.DiveOrganisations.Where(o => o.OrganisationCode == code).FirstOrDefaultAsync();

        if (org == null)
        {
            return NotFound();
        }
        return Ok(org);
    }
}
