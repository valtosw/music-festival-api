using MusicFestival.Core.Enums;

namespace MusicFestival.Core.Entities
{
    public sealed class Ticket
    {
        public int Id { get; set; }
        public TicketType Type { get; set; } 
        public decimal Price { get; set; }
        public int QuantityAvailable { get; set; }

        public int FestivalId { get; set; }
        public Festival? Festival { get; set; }
    }
}
