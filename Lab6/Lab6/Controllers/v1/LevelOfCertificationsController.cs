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
public class LevelOfCertificationsController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    public LevelOfCertificationsController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetCertifications()
    {
        var certifications = await _context.LevelsOfCertification.ToListAsync();

        return Ok(certifications);
    }

    [HttpGet("{code}")]
    public async Task<IActionResult> GetCertification(string code)
    {
        var cert = await _context.LevelsOfCertification.FindAsync(code);

        if (cert == null)
        {
            return NotFound();
        }

        return Ok(cert);
    }
}
