namespace MusicFestival.Core.Entities
{
    public sealed class Artist
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Genre { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

        public ICollection<Performance> Performances { get; set; } = [];
    }
}
