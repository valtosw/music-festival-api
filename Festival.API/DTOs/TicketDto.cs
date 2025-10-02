using MusicFestival.Core.Enums;

namespace MusicFestival.API.DTOs
{
    public sealed class TicketDto
    {
        public int Id { get; set; }
        public TicketType Type { get; set; }
        public decimal Price { get; set; }
        public int QuantityAvailable { get; set; }
        public int FestivalId { get; set; }
    }
}
