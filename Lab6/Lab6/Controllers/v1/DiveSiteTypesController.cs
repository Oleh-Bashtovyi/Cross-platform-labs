﻿using Lab6.Data;
using Lab6.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Lab6.Controllers.v1;

[Authorize]
[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
public class DiveSiteTypesController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    public DiveSiteTypesController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<DiveSiteType>>> GetDiveSiteTypes()
    {
        return await _context.DiveSiteTypes.ToListAsync();
    }

    [HttpGet("{code}")]
    public async Task<ActionResult<DiveSiteType>> GetDiveSiteType(string code)
    {
        var type = await _context.DiveSiteTypes.FindAsync(code);
        if (type == null)
            return NotFound();
        return type;
    }

    [HttpPost]
    public async Task<ActionResult<DiveSiteType>> CreateDiveSiteType(DiveSiteType type)
    {
        _context.DiveSiteTypes.Add(type);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetDiveSiteType), new { code = type.DiveSiteCode }, type);
    }
}