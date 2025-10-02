using Microsoft.Extensions.Configuration;
using MusicFestival.Core.Services.Abstractions;

namespace MusicFestival.Core.Services
{
    public sealed class TelegramService(HttpClient httpClient, IConfiguration configuration) : ITelegramService
    {
        private readonly string _botToken = configuration["Telegram:BotToken"]!;
        private readonly string _chatId = configuration["Telegram:ChatId"]!;

        public async Task SendMessageAsync(string message)
        {
            var url = $"https://api.telegram.org/bot{_botToken}/sendMessage?chat_id={_chatId}&text={message}";
            var response = await httpClient.GetAsync(url);

            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Failed to send message via Telegram. Status Code: {response.StatusCode}, Response: {errorContent}");
            }
        }
    }
}
