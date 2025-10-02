namespace MusicFestival.API.DTOs
{
    public sealed class StageDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int FestivalId { get; set; }
    }
}
