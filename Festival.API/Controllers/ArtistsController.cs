using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MusicFestival.API.DTOs;
using MusicFestival.Core.Entities;
using MusicFestival.Data;

namespace MusicFestival.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArtistsController(AppDbContext context, IMapper mapper) : BaseApiController
    {
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ArtistDto>>> GetArtists()
        {
            var artists = await context.Artists.ToListAsync();

            return Ok(mapper.Map<IEnumerable<ArtistDto>>(artists));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ArtistDto>> GetArtist(int id)
        {
            var artist = await context.Artists.FindAsync(id);
            if (artist is null) return NotFound();

            return Ok(mapper.Map<ArtistDto>(artist));
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<ArtistDto>> CreateArtist(ArtistDto artistDto)
        {
            var artist = mapper.Map<Artist>(artistDto);

            context.Artists.Add(artist);
            await context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetArtist), new { id = artist.Id }, mapper.Map<ArtistDto>(artist));
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> UpdateArtist(int id, ArtistDto artistDto)
        {
            if (id != artistDto.Id) return BadRequest();

            var artist = mapper.Map<Artist>(artistDto);
            context.Entry(artist).State = EntityState.Modified;
            await context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteArtist(int id)
        {
            var artist = await context.Artists.FindAsync(id);

            if (artist is null) return NotFound();
            
            context.Artists.Remove(artist);
            await context.SaveChangesAsync();

            return NoContent();
        }
    }
}
