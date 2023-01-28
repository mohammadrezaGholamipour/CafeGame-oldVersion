using CafeGameApi.Context;
using CafeGameApi.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CafeGameApi.Controllers;

[Route("api/hourRate")]
[ApiController]
public class HourRateController : ControllerBase
{
    private readonly AppDbContext _context;

    public HourRateController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> Index([FromQuery] int? id)
    {
        return Ok(await _context.HourRates
            .AsNoTracking()
            .Where(x => !id.HasValue || x.Id == id.Value)
            .ToListAsync());
    }


    [HttpPost]
    public async Task<IActionResult> Add([FromBody] HourRate hourRate)
    {
        if (await _context.HourRates.AnyAsync(x => x.Rate == hourRate.Rate))
            return (Conflict());

        var model = await _context.HourRates.AddAsync(hourRate);
        var result = await _context.SaveChangesAsync();

        return result > 0 ? Ok(model.Entity.Id) : NoContent();
    }


    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        var model = await _context.HourRates.FirstOrDefaultAsync(x => x.Id == id);

        if (model is null)
            return NotFound();

        _context.HourRates.Remove(model);
        var result = await _context.SaveChangesAsync();

        return result > 0 ? Ok() : NoContent();
    }
}

