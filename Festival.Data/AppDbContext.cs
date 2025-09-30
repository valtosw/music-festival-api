using MusicFestival.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace MusicFestival.Data
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        public DbSet<Artist> Artists { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Festival> Festivals { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Performance> Performances { get; set; }
        public DbSet<Stage> Stages { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
    }
}
