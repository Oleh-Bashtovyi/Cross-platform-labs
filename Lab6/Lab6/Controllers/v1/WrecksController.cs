using Lab6.Data;
using Lab6.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Lab6.Controllers.v1
{
    [Authorize]
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class WreckController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public WreckController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Wreck>>> GetWrecks()
        {
            return await _context.Wrecks.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Wreck>> GetWreck(Guid id)
        {
            var wreck = await _context.Wrecks.FindAsync(id);

            if (wreck == null)
            {
                return NotFound();
            }
            return wreck;
        }

        [HttpPost]
        public async Task<ActionResult<Wreck>> CreateWreck(Wreck wreck)
        {
            _context.Wrecks.Add(wreck);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetWreck), new { id = wreck.DiveSiteId }, wreck);
        }
    }
}
