using CafeGameApi.ConfigModels;
using CafeGameApi.Context;
using CafeGameApi.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;
namespace CafeGameApi.Controllers;

[Route("api/system")]
[ApiController]
[Authorize(Roles = AppConstants.UserRoles.RegisteredUser)]
public class SystemController : AppBaseUserController
{
    private readonly AppDbContext _context;

    public SystemController(UserManager<IdentityUser<int>> userManager,
        IHttpContextAccessor contextAccessor,
        AppDbContext context)
        : base(userManager, contextAccessor)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> Index([FromQuery] int? id)
    {
        return Ok(await _context.PSSystems
            .AsNoTracking()
            .Where(x => x.UserId == this.AppUserId)
            .Where(x => !id.HasValue || x.Id == id.Value)
            .ToListAsync());
    }

    [HttpPost]
    public async Task<IActionResult> Add([FromBody] PSSystem pSSystem)
    {
        // if (await _context.PSSystems.AnyAsync(x => x.Name == pSSystem.Name))
        //     return (Conflict());
        pSSystem.UserId = this.AppUserId;
        var model = await _context.PSSystems.AddAsync(pSSystem);
        var result = await _context.SaveChangesAsync();

        return result > 0 ? Ok(model.Entity.Id) : NoContent();
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update([FromRoute] int id, [FromBody] string name)
    {
        var model = await _context.PSSystems
            .Where(x => x.UserId == this.AppUserId)
            .FirstOrDefaultAsync(x => x.Id == id);

        if (model is null /*|| (await _context.PSSystems
                .Where(x => x.UserId == this.AppUserId)
                .AnyAsync(x => x.Name == name))*/)
            return NotFound();

        model.Name = name;
        var result = await _context.SaveChangesAsync();

        return result > 0 ? Ok(model) : NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        var model = await _context.PSSystems
            .Where(x => x.UserId == this.AppUserId)
            .FirstOrDefaultAsync(x => x.Id == id);

        if (model is null)
            return NotFound();

        _context.PSSystems.Remove(model);
        var result = await _context.SaveChangesAsync();

        return result > 0 ? Ok() : NoContent();
    }
}

