using CafeGameApi.Context;
using CafeGameApi.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CafeGameApi.Controllers;

[Route("api/food")]
[ApiController]
public class FoodController : ControllerBase
{
    private readonly AppDbContext _context;

    public FoodController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> Index([FromQuery] int? id)
    {
        return Ok(await _context.Foods
            .AsNoTracking()
            .Where(x => !id.HasValue || x.Id == id.Value)
            .ToListAsync());
    }


    [HttpPost]
    public async Task<IActionResult> Add([FromBody] Food food)
    {
        if (await _context.Foods.AnyAsync(x => x.Name == food.Name))
            return (Conflict());

        var model = await _context.Foods.AddAsync(food);
        var result = await _context.SaveChangesAsync();

        return result > 0 ? Ok(model.Entity.Id) : NoContent();
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update([FromRoute] int id, [FromBody] Food food)
    {
        var model = await _context.Foods.FirstOrDefaultAsync(x => x.Id == id);

        if (model is null || (await _context.Foods.AnyAsync(x => x.Name == food.Name)))
            return NotFound();

        model.Name = food.Name;
        model.Cost = food.Cost;
        var result = await _context.SaveChangesAsync();

        return result > 0 ? Ok(model) : NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        var model = await _context.Foods.FirstOrDefaultAsync(x => x.Id == id);

        if (model is null)
            return NotFound();

        _context.Foods.Remove(model);
        var result = await _context.SaveChangesAsync();

        return result > 0 ? Ok() : NoContent();
    }
}