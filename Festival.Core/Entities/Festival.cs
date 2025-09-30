namespace MusicFestival.Core.Entities
{
    public sealed class Festival
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Description { get; set; } = string.Empty;

        public int CountryId { get; set; }
        public Country Country { get; set; } = null!;

        public ICollection<Stage> Stages { get; set; } = [];
        public ICollection<Ticket> Tickets { get; set; } = [];
    }
}
