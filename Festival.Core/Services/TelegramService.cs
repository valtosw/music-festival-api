using Microsoft.Extensions.Configuration;
using MusicFestival.Core.Services.Abstractions;

namespace MusicFestival.Core.Services
{
    public sealed class TelegramService : ITelegramService
    {
        private readonly HttpClient _httpClient;
        private readonly string _botToken;
        private readonly string _chatId;

        public TelegramService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _botToken = configuration["Telegram:BotToken"] ?? throw new ArgumentException(nameof(_botToken));
            _chatId = configuration["Telegram:ChatId"] ?? throw new ArgumentException(nameof(_chatId));
        }

        public async Task SendMessageAsync(string message)
        {
            var url = $"https://api.telegram.org/bot{_botToken}/sendMessage?chat_id={_chatId}&text={message}";
            var response = await _httpClient.GetAsync(url);

            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Failed to send message via Telegram. Status Code: {response.StatusCode}, Response: {errorContent}");
            }
        }
    }
}
