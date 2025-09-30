namespace MusicFestival.Core.Entities
{
    public sealed class Stage
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;

        public int FestivalId { get; set; }
        public Festival? Festival { get; set; }

        public ICollection<Performance> Performances { get; set; } = [];
    }
}
