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
    public async Task<ActionResult<IEnumerable<LevelOfCertification>>> GetCertifications()
    {
        return await _context.LevelsOfCertification.ToListAsync();
    }

    [HttpGet("{code}")]
    public async Task<ActionResult<LevelOfCertification>> GetCertification(string code)
    {
        var cert = await _context.LevelsOfCertification.FindAsync(code);

        if (cert == null)
        {
            return NotFound();
        }

        return cert;
    }

    [HttpPost]
    public async Task<ActionResult<LevelOfCertification>> CreateCertification(LevelOfCertification cert)
    {
        _context.LevelsOfCertification.Add(cert);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetCertification), new { code = cert.CertificationCode }, cert);
    }
}
