using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MusicFestival.API.DTOs;
using MusicFestival.Core.Entities;
using MusicFestival.Core.Services;
using MusicFestival.Core.Services.Abstractions;
using MusicFestival.Data;

namespace MusicFestival.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketsController(AppDbContext context, IMapper mapper, ITelegramService telegramService) : BaseApiController
    {
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<TicketDto>>> GetTickets()
        {
            var tickets = await context.Tickets.ToListAsync();
            return Ok(mapper.Map<IEnumerable<TicketDto>>(tickets));
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<TicketDto>> GetTicket(int id)
        {
            var ticket = await context.Tickets.FindAsync(id);
            if (ticket == null) return NotFound();
            return Ok(mapper.Map<TicketDto>(ticket));
        }

        [HttpPost("{id}/buy")]
        [Authorize] 
        public async Task<IActionResult> BuyTicket(int id)
        {
            var ticket = await context.Tickets
                .Include(t => t.Festival) 
                .FirstOrDefaultAsync(t => t.Id == id);

            if (ticket is null) return NotFound("Ticket not found.");

            if (ticket.QuantityAvailable <= 0) return BadRequest("This ticket is sold out.");

            ticket.QuantityAvailable--;
            await context.SaveChangesAsync();

            var message = $"A ticket has been sold!\n" +
                          $"Festival: {ticket.Festival!.Name}\n" +
                          $"Ticket Type: {ticket.Type}\n" +
                          $"Price: ${ticket.Price}\n" +
                          $"Remaining tickets of this type: {ticket.QuantityAvailable}";

            await telegramService.SendMessageAsync(message);

            return Ok(new { message = "Purchase successful!", remainingTickets = ticket.QuantityAvailable });
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<TicketDto>> CreateTicket(TicketDto ticketDto)
        {
            var ticket = mapper.Map<Ticket>(ticketDto);
            context.Tickets.Add(ticket);
            await context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetTicket), new { id = ticket.Id }, mapper.Map<TicketDto>(ticket));
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> UpdateTicket(int id, TicketDto ticketDto)
        {
            if (id != ticketDto.Id) return BadRequest();

            var ticket = mapper.Map<Ticket>(ticketDto);
            context.Entry(ticket).State = EntityState.Modified;
            await context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteTicket(int id)
        {
            var ticket = await context.Tickets.FindAsync(id);

            if (ticket is null) return NotFound();

            context.Tickets.Remove(ticket);
            await context.SaveChangesAsync();
            
            return NoContent();
        }
    }
}
