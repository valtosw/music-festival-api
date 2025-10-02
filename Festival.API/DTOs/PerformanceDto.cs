namespace MusicFestival.API.DTOs
{
    public sealed class PerformanceDto
    {
        public int Id { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public int StageId { get; set; }
        public int ArtistId { get; set; }
    }
}
