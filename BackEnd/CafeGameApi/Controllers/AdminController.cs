using CafeGameApi.ConfigModels;
using CafeGameApi.Context;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace CafeGameApi.Controllers;

[Route("api/__admin")]
[ApiController]
[Authorize(Roles = AppConstants.UserRoles.Admin)]
public class AdminController : AppBaseUserController
{
    private readonly AppDbContext _context;
    private readonly UserManager<IdentityUser<int>> _userManager;

    public AdminController(AppDbContext context, 
        UserManager<IdentityUser<int>> userManager, 
        IHttpContextAccessor contextAccessor) 
        : base(userManager, contextAccessor)
    {
        _context = context;
        _userManager = userManager;
    }

    [HttpGet("registered-users")]
    public async Task<IActionResult> GetRegisteredUsers() => 
        Ok(await _userManager.GetUsersInRoleAsync(AppConstants.UserRoles.RegisteredUser));

    [HttpGet("banned-users")]
    public async Task<IActionResult> GetBannedUsers() =>
        Ok(await _userManager.GetUsersInRoleAsync(AppConstants.UserRoles.Banned));

    [HttpPut("ban-user/{id:int}")]
    public async Task<IActionResult> BanUser([FromRoute] int id)
    {
        if (id == base.AppUserId)
        {
            return BadRequest();
        }
        var user = await _userManager.FindByIdAsync(id.ToString());
        if (user is null)
        {
            return NotFound();
        }

        var roles = await _userManager.GetRolesAsync(user);
        await _userManager.RemoveFromRolesAsync(user, roles);
        await _userManager.AddToRoleAsync(user, AppConstants.UserRoles.Banned);

        return Ok();
    }

    [HttpPut("un-ban-user/{id:int}")]
    public async Task<IActionResult> UnBanUser([FromRoute] int id)
    {
        if (id == base.AppUserId)
        {
            return BadRequest();
        }
        var user = await _userManager.FindByIdAsync(id.ToString());
        if (user is null)
        {
            return NotFound();
        }

        var roles = await _userManager.GetRolesAsync(user);
        await _userManager.RemoveFromRolesAsync(user, roles);
        await _userManager.AddToRoleAsync(user, AppConstants.UserRoles.RegisteredUser);

        return Ok();
    }

    [HttpGet("user-bills/{userId:int}")]
    public async Task<IActionResult> GetBillsBySystem([FromRoute] int userId,
        [FromQuery] int? systemId, 
        [FromQuery] DateTime? startDate, 
        [FromQuery] DateTime? endDate)
    {
        var bills = await _context.Bills
            .AsNoTracking()
            .Where(x => x.UserId == this.AppUserId)
            .Where(x => systemId == null || x.SystemId == systemId)
            .Where(x => startDate == null || x.StartTime > startDate)
            .Where(x => endDate == null || x.EndTime < endDate)
            .Include(x=>x.BillFoods)
            .ThenInclude(x=>x.Food)
            .ToListAsync();

        return Ok(bills);
    }
}