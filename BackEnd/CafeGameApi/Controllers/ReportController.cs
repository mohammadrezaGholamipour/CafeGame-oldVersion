using CafeGameApi.ConfigModels;
using CafeGameApi.Context;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace CafeGameApi.Controllers
{
    [Route("api/report")]
    [ApiController]
    [Authorize(Roles = AppConstants.UserRoles.RegisteredUser)]
    public class ReportController : AppBaseUserController
    {
        private readonly AppDbContext _context;

        public ReportController(UserManager<IdentityUser<int>> userManager,
            IHttpContextAccessor contextAccessor,
            AppDbContext context)
            : base(userManager, contextAccessor)
        {
            _context = context;
        }

        [HttpGet("system-bills")]
        public async Task<IActionResult> GetBillsBySystem([FromQuery] int? systemId, [FromQuery] DateTime? startDate, [FromQuery] DateTime? endDate)
        {
            var bills = await _context.Bills
                .AsNoTracking()
                .Where(x => x.UserId == this.AppUserId)
                .Where(x => systemId == null || x.SystemId == systemId)
                .Where(x => startDate == null || x.StartTime > startDate)
                .Where(x => endDate == null || x.EndTime < endDate)
                .ToListAsync();

            return Ok(bills);
        }

        [HttpGet("food-total")]
        public IActionResult GetFoodSold([FromQuery] int foodId, [FromQuery] DateTime? startDate, [FromQuery] DateTime? endDate)
        {
            var total = _context.Bills
                .AsNoTracking()
                .Where(x => x.UserId == this.AppUserId)
                .Where(x => startDate == null || x.StartTime > startDate)
                .Where(x => endDate == null || x.EndTime < endDate)
                .Include(x => x.BillFoods)
                .ThenInclude(x => x.Food)
                .Where(x => x.BillFoods.Any(y => y.FoodId == foodId))
                .Select(x => x.BillFoods.First(y => y.FoodId == foodId).Count)
                .Aggregate((x, y) => x + y);

            return Ok(total);
        }

    }
}
