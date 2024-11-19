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
public class DiverCertificationsController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    public DiverCertificationsController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<DiverCertification>>> GetDiverCertifications()
    {
        var diverCertifications = await _context.DiverCertifications.ToListAsync();

        return diverCertifications;
    }

    [HttpGet("{diverId}/{certificationCode}")]
    public async Task<ActionResult<DiverCertification>> GetDiverCertification(Guid diverId, string certificationCode)
    {
        var cert = await _context.DiverCertifications.FindAsync(diverId, certificationCode);

        if (cert == null)
        {
            return NotFound();
        }
        return cert;
    }
}
