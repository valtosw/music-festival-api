using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MusicFestival.API.DTOs;
using MusicFestival.Core.Entities;
using MusicFestival.Data;

namespace MusicFestival.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PerformancesController(AppDbContext context, IMapper mapper) : BaseApiController
    {
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<PerformanceDto>>> GetPerformances()
        {
            var performances = await context.Performances.ToListAsync();
            return Ok(mapper.Map<IEnumerable<PerformanceDto>>(performances));
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<PerformanceDto>> GetPerformance(int id)
        {
            var performance = await context.Performances.FindAsync(id);
            if (performance is null) return NotFound();
            return Ok(mapper.Map<PerformanceDto>(performance));
        }

        [HttpPost]
        [Authorize(Policy = "AdminOnly")]
        public async Task<ActionResult<PerformanceDto>> CreatePerformance(PerformanceDto performanceDto)
        {
            var performance = mapper.Map<Performance>(performanceDto);
            context.Performances.Add(performance);
            await context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetPerformance), new { id = performance.Id }, mapper.Map<PerformanceDto>(performance));
        }

        [HttpPut("{id}")]
        [Authorize(Policy = "AdminOnly")]
        public async Task<IActionResult> UpdatePerformance(int id, PerformanceDto performanceDto)
        {
            if (id != performanceDto.Id) return BadRequest();

            var performance = mapper.Map<Performance>(performanceDto);
            context.Entry(performance).State = EntityState.Modified;
            await context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Policy = "AdminOnly")]
        public async Task<IActionResult> DeletePerformance(int id)
        {
            var performance = await context.Performances.FindAsync(id);

            if (performance is null) return NotFound();

            context.Performances.Remove(performance);
            await context.SaveChangesAsync();
            
            return NoContent();
        }
    }
}
