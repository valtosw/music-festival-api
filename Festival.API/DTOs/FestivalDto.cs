namespace MusicFestival.API.DTOs
{
    public sealed class FestivalDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Description { get; set; } = string.Empty;
        public int CountryId { get; set; }
    }
}
