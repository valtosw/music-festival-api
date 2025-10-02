namespace MusicFestival.Core.Services.Abstractions
{
    public interface ITelegramService
    {
        Task SendMessageAsync(string message);
    }
}
