using CafeGameApi.ConfigModels;
using CafeGameApi.Context;
using CafeGameApi.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;
namespace CafeGameApi.Controllers;

[Route("api/food")]
[ApiController]
[Authorize(Roles = AppConstants.UserRoles.RegisteredUser)]
public class FoodController : AppBaseUserController
{
    private readonly AppDbContext _context;

    public FoodController(UserManager<IdentityUser<int>> userManager,
        IHttpContextAccessor contextAccessor,
        AppDbContext context)
        : base(userManager, contextAccessor)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> Index([FromQuery] int? id)
    {
        return Ok(await _context.Foods
            .Where(x => x.UserId == this.AppUserId)
            .AsNoTracking()
            .Where(x => !id.HasValue || x.Id == id.Value)
            .ToListAsync());
    }


    [HttpPost]
    public async Task<IActionResult> Add([FromBody] Food food)
    {
        if (await _context.Foods
                .Where(x => x.UserId == this.AppUserId)
                .AnyAsync(x => x.Name == food.Name))
            return (Conflict());

        food.UserId = this.AppUserId;
        var model = await _context.Foods.AddAsync(food);
        var result = await _context.SaveChangesAsync();

        return result > 0 ? Ok(model.Entity.Id) : NoContent();
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update([FromRoute] int id, [FromBody] Food food)
    {
        var model = await _context.Foods
            .Where(x => x.UserId == this.AppUserId)
            .FirstOrDefaultAsync(x => x.Id == id);

        if (model is null)
            return NotFound();

        model.Name = food.Name;
        model.Cost = food.Cost;
        var result = await _context.SaveChangesAsync();

        return result > 0 ? Ok(model) : NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        var model = await _context.Foods
            .Where(x => x.UserId == this.AppUserId)
            .FirstOrDefaultAsync(x => x.Id == id);

        if (model is null)
            return NotFound();

        _context.Foods.Remove(model);
        var result = await _context.SaveChangesAsync();

        return result > 0 ? Ok() : NoContent();
    }
}