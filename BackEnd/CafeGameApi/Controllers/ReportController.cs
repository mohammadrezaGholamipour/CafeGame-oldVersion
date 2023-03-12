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

        [HttpGet]
        public async Task<IActionResult> GetBySystem([FromQuery] int? systemId, [FromQuery] DateTime? startDate, [FromQuery] DateTime? endDate)
        {
            var bills = await _context.Bills
                .Where(x => x.UserId == this.AppUserId)
                .Where(x => systemId == null || x.SystemId == systemId)
                .Where(x => startDate == null || x.StartTime > startDate)
                .Where(x => endDate == null || x.EndTime < endDate)
                .AsNoTracking()
                .ToListAsync();

            return Ok(bills);
        }
    }
}
