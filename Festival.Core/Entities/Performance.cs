namespace MusicFestival.Core.Entities
{
    public sealed class Performance
    {
        public int Id { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }

        public int StageId { get; set; }
        public Stage? Stage { get; set; }
        public int ArtistId { get; set; }
        public Artist? Artist { get; set; }
    }
}
