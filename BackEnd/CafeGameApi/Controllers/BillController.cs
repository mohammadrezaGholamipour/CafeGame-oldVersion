using CafeGameApi.ConfigModels;
using CafeGameApi.Context;
using CafeGameApi.Entities;
using CafeGameApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CafeGameApi.Controllers;

[Route("api/bill")]
[ApiController]
[Authorize(Roles = AppConstants.UserRoles.RegisteredUser)]
public class BillController : AppBaseUserController
{
    private readonly AppDbContext _context;


    public BillController(UserManager<IdentityUser<int>> userManager,
        IHttpContextAccessor contextAccessor,
        AppDbContext context)
        : base(userManager, contextAccessor)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> Index([FromQuery] int? id)
    {
        return Ok(await _context.Bills
            .AsNoTracking()
            .Where(x => x.UserId == this.AppUserId)
            .Include(x => x.BillFoods)
            .ThenInclude(x => x.Food)
            .Where(x => !id.HasValue || x.Id == id.Value)
            .ToListAsync());
    }


    [HttpPost("{systemId:int}/{rateId:int}")]
    public async Task<IActionResult> Create([FromRoute] int systemId, [FromRoute] int rateId, [FromBody] DateTime starTime)
    {
        if (!(await _context.PSSystems
                .Where(x => x.UserId == this.AppUserId)
                .AnyAsync(x => x.Id == systemId)) ||
            !(await _context.HourRates
                .Where(x => x.UserId == this.AppUserId)
                .AnyAsync(x => x.Id == rateId)))
        {
            return NotFound();
        }

        var model = await _context.Bills.AddAsync(new Bill()
        {
            UserId = this.AppUserId,
            StartTime = starTime,
            SystemId = systemId,
            HourRateId = rateId
        });

        var result = await _context.SaveChangesAsync();

        return result > 0 ? Ok(model.Entity.Id) : NoContent();
    }

    [HttpPut("{id:int}/[action]")]
    public async Task<IActionResult> SetFoods([FromRoute] int id, [FromBody] List<SetFoodModel> foods)
    {
        var foodIds = foods.Select(x => x.FoodId).ToList();

        var userFoods = await _context.Foods
            .Where(x => x.UserId == this.AppUserId)
            .AsNoTracking()
            .Select(x => x.Id)
            .ToListAsync();

        if (!(await _context.Bills
                .Where(x => x.UserId == this.AppUserId)
                .AnyAsync(x => x.Id == id)) ||
            foodIds.Any(x => !userFoods.Contains(x)))
        {
            return NotFound();
        }

        _context.BillFoods.RemoveRange(_context.BillFoods.Where(x => x.BillId == id));

        await _context.BillFoods.AddRangeAsync(foods.Select(x => new BillFood()
        {
            Count = x.Count,
            FoodId = x.FoodId,
            BillId = id
        }));


        var result = await _context.SaveChangesAsync();

        return result > 0 ? Ok(result) : NoContent();
    }

    [HttpPut("{id:int}/[action]")]
    public async Task<IActionResult> Close([FromRoute] int id, [FromBody] DateTime endTime)
    {
        var model = await _context.Bills
            .Where(x => x.UserId == this.AppUserId)
            .Include(x => x.HourRate)
            .FirstOrDefaultAsync(x => x.Id == id);

        if (model is null)
            return NotFound();
        if (model.StartTime > endTime)
            return BadRequest("ساعت پایان نباید قبل از ساعت شروع باشد");

        model.EndTime = endTime;

        var foodCost = 0;
        (await _context.BillFoods
            .Where(x => x.BillId == id)
            .Select(x => x.Food!.Cost * x.Count)
            .ToListAsync())
            .ForEach(x => foodCost += x);


        var minuteRate = ((float)(model.HourRate!.Rate)) / 60f;

        var totalMinutes = (model.EndTime - model.StartTime).Value.TotalMinutes;

        if (totalMinutes < 60)
            totalMinutes = 60;

        var timeCost = Convert.ToInt32(totalMinutes * minuteRate);

        model.FinalCost = foodCost + timeCost;

        var result = await _context.SaveChangesAsync();

        return result > 0 ? Ok(model.FinalCost) : NoContent();
    }



    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        var model = await _context.Bills
            .Where(x => x.UserId == this.AppUserId)
            .FirstOrDefaultAsync(x => x.Id == id);

        if (model is null)
            return NotFound();

        _context.Bills.Remove(model);
        var result = await _context.SaveChangesAsync();

        return result > 0 ? Ok() : NoContent();
    }

    [HttpGet("/opens")]
    public async Task<IActionResult> Index()
    {
        return Ok(await _context.Bills
            .Where(x => x.UserId == this.AppUserId)
            .AsNoTracking()
            .Include(x => x.BillFoods)
            .ThenInclude(x => x.Food)
            .Where(x => x.EndTime == null)
            .ToListAsync());
    }
}

