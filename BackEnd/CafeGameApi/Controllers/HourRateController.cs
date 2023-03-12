using CafeGameApi.ConfigModels;
using CafeGameApi.Context;
using CafeGameApi.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;
namespace CafeGameApi.Controllers;

[Route("api/hourRate")]
[ApiController]
[Authorize(Roles = AppConstants.UserRoles.RegisteredUser)]
public class HourRateController : AppBaseUserController
{
    private readonly AppDbContext _context;

    public HourRateController(UserManager<IdentityUser<int>> userManager,
        IHttpContextAccessor contextAccessor,
        AppDbContext context)
        : base(userManager, contextAccessor)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> Index([FromQuery] int? id)
    {
        return Ok(await _context.HourRates
            .AsNoTracking()
            .Where(x => x.UserId == this.AppUserId)
            .Where(x => !id.HasValue || x.Id == id.Value)
            .ToListAsync());
    }


    [HttpPost]
    public async Task<IActionResult> Add([FromBody] HourRate hourRate)
    {
        if (await _context.HourRates
                .Where(x => x.UserId == this.AppUserId)
                .AnyAsync(x => x.Rate == hourRate.Rate))
        {
            return Conflict();
        }
        hourRate.UserId = this.AppUserId;
        var model = await _context.HourRates.AddAsync(hourRate);
        var result = await _context.SaveChangesAsync();

        return result > 0 ? Ok(model.Entity.Id) : NoContent();
    }


    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        var model = await _context.HourRates
            .Where(x => x.UserId == this.AppUserId)
            .FirstOrDefaultAsync(x => x.Id == id);

        if (model is null)
            return NotFound();

        _context.HourRates.Remove(model);
        var result = await _context.SaveChangesAsync();

        return result > 0 ? Ok() : NoContent();
    }
}

