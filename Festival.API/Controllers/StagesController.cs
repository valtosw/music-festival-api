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
    public class StagesController(AppDbContext context, IMapper mapper) : BaseApiController
    {
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<StageDto>>> GetStages()
        {
            var stages = await context.Stages.ToListAsync();
            return Ok(mapper.Map<IEnumerable<StageDto>>(stages));
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<StageDto>> GetStage(int id)
        {
            var stage = await context.Stages.FindAsync(id);
            if (stage is null) return NotFound();
            return Ok(mapper.Map<StageDto>(stage));
        }

        [HttpPost]
        [Authorize(Policy = "AdminOnly")]
        public async Task<ActionResult<StageDto>> CreateStage(StageDto stageDto)
        {
            var stage = mapper.Map<Stage>(stageDto);
            context.Stages.Add(stage);
            await context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetStage), new { id = stage.Id }, mapper.Map<StageDto>(stage));
        }

        [HttpPut("{id}")]
        [Authorize(Policy = "AdminOnly")]
        public async Task<IActionResult> UpdateStage(int id, StageDto stageDto)
        {
            if (id != stageDto.Id) return BadRequest();

            var stage = mapper.Map<Stage>(stageDto);
            context.Entry(stage).State = EntityState.Modified;
            await context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Policy = "AdminOnly")]
        public async Task<IActionResult> DeleteStage(int id)
        {
            var stage = await context.Stages.FindAsync(id);

            if (stage is null) return NotFound();

            context.Stages.Remove(stage);
            await context.SaveChangesAsync();

            return NoContent();
        }
    }
}
