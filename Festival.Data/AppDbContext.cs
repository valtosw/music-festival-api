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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Festival>()
                .HasOne(f => f.Country)
                .WithMany(c => c.Festivals)
                .HasForeignKey(f => f.CountryId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Stage>()
                .HasOne(s => s.Festival)
                .WithMany(f => f.Stages)
                .HasForeignKey(s => s.FestivalId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Performance>()
                .HasOne(p => p.Artist)
                .WithMany(a => a.Performances)
                .HasForeignKey(p => p.ArtistId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Performance>()
                .HasOne(p => p.Stage)
                .WithMany(s => s.Performances)
                .HasForeignKey(p => p.StageId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
