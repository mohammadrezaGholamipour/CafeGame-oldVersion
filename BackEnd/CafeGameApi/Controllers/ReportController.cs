using CafeGameApi.Context;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CafeGameApi.Controllers
{
    [Route("api/report")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ReportController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetBySystem([FromQuery] int? systemId, [FromQuery] DateTime? startDate, [FromQuery] DateTime? endDate)
        {
            var bills =await _context.Bills
                .Where(x => systemId == null || x.SystemId == systemId)
                .Where(x => startDate == null || x.StartTime > startDate)
                .Where(x => endDate == null || x.EndTime < endDate)
                .AsNoTracking()
                .ToListAsync();

            return Ok(bills);
        }
    }
}
