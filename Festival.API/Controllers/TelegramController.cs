using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MusicFestival.Core.Services.Abstractions;

namespace MusicFestival.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TelegramController(ITelegramService telegramService) : BaseApiController
    {
        [HttpPost("notify")]
        [Authorize(Policy = "AdminOnly")]
        public async Task<IActionResult> Notify([FromBody] string message)
        {
            await telegramService.SendMessageAsync(message);
            return Ok();
        }
    }
}
