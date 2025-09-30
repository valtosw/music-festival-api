namespace MusicFestival.Core.Entities
{
    public sealed class Genre
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;

        public ICollection<Artist> Artists { get; set; } = [];
    }
}
