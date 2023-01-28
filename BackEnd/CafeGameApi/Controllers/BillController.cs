﻿using CafeGameApi.Context;
using CafeGameApi.Entities;
using CafeGameApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CafeGameApi.Controllers;

[Route("api/bill")]
[ApiController]
public class BillController : ControllerBase
{
    private readonly AppDbContext _context;

    public BillController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> Index([FromQuery] int? id)
    {
        return Ok(await _context.Bills
            .AsNoTracking()
            .Where(x => !id.HasValue || x.Id == id.Value)
            .ToListAsync());
    }


    [HttpPost("{systemId:int}/{rateId:int}")]
    public async Task<IActionResult> Create([FromRoute] int systemId, [FromRoute] int rateId, [FromBody] DateTime starTime)
    {
        if (!(await _context.PSSystems.AnyAsync(x => x.Id == systemId)))
            return NotFound();

        var model = await _context.Bills.AddAsync(new Bill()
        {
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
        if (!(await _context.Bills.AnyAsync(x => x.Id == id)) ||
            !(await _context.Foods.AnyAsync(x => foods.Any(y => y.FoodId == x.Id))))
            return NotFound();

        foreach (var food in foods)
        {
            await _context.BillFoods.AddAsync(new BillFood
            {
                Count = food.Count,
                FoodId = food.FoodId,
                BillId = id
            });
        }

        var result = await _context.SaveChangesAsync();

        return result > 0 ? Ok(result) : NoContent();
    }

    [HttpPut("{id:int}/[action]")]
    public async Task<IActionResult> Close([FromRoute] int id, [FromBody] DateTime endTime)
    {
        var model = await _context.Bills
            .Include(x => x.HourRate)
            .FirstOrDefaultAsync(x => x.Id == id);

        if (model is null)
            return NotFound();
        if (model.StartTime > endTime)
            return BadRequest("ساعت پایان نباید قبل از ساعت شروع باشد");

        model.EndTime = endTime;

        var foodCost = _context.BillFoods.Where(x => x.BillId == id)
            .Select(x => x.Food!.Cost * x.Count)
            .Aggregate((x, y) => x + y);

        var minuteRate = ((float)(model.HourRate!.Rate)) / 60f;

        var timeCost = Convert.ToInt32((model.EndTime - model.StartTime).Value.TotalMinutes * minuteRate);

        model.FinalCost = foodCost + timeCost;

        var result = await _context.SaveChangesAsync();

        return result > 0 ? Ok(model.FinalCost) : NoContent();
    }



    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        var model = await _context.Bills.FirstOrDefaultAsync(x => x.Id == id);

        if (model is null)
            return NotFound();

        _context.Bills.Remove(model);
        var result = await _context.SaveChangesAsync();

        return result > 0 ? Ok() : NoContent();
    }
}

