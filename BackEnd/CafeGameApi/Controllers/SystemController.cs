using CafeGameApi.Context;
using CafeGameApi.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CafeGameApi.Controllers;

[Route("api/system")]
[ApiController]
public class SystemController : ControllerBase
{
    private readonly AppDbContext _context;

    public SystemController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> Index([FromQuery] int? id)
    {
        return Ok(await _context.PSSystems
            .AsNoTracking()
            .Where(x => !id.HasValue || x.Id == id.Value)
            .ToListAsync());
    }

    [HttpPost]
    public async Task<IActionResult> Add([FromBody] PSSystem pSSystem)
    {
        if (await _context.PSSystems.AnyAsync(x => x.Name == pSSystem.Name))
            return (Conflict());

        var model = await _context.PSSystems.AddAsync(pSSystem);
        var result = await _context.SaveChangesAsync();

        return result > 0 ? Ok(model.Entity.Id) : NoContent();
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update([FromRoute] int id, [FromBody] string name)
    {
        var model = await _context.PSSystems.FirstOrDefaultAsync(x => x.Id == id);

        if (model is null || (await _context.PSSystems.AnyAsync(x => x.Name == name)))
            return NotFound();

        model.Name = name;
        var result = await _context.SaveChangesAsync();

        return result > 0 ? Ok(model) : NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        var model = await _context.PSSystems.FirstOrDefaultAsync(x => x.Id == id);

        if (model is null)
            return NotFound();

        _context.PSSystems.Remove(model);
        var result = await _context.SaveChangesAsync();

        return result > 0 ? Ok() : NoContent();
    }
}

