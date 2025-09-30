namespace MusicFestival.Data.Datasets
{
    public class DataSeeder(AppDbContext context)
    {
        private readonly AppDbContext _context = context;

        public async Task SeedAsync()
        {
            if (!_context.Genres.Any())
            {
                var genres = DatasetHandler.GetGenreData(Path.Combine(Directory.GetCurrentDirectory(), "Datasets", "DatasetFiles", "genres_dataset.txt"));
                await _context.Genres.AddRangeAsync(genres);
            }

            if (!_context.Countries.Any())
            {
                var countries = DatasetHandler.GetCountryData(Path.Combine(Directory.GetCurrentDirectory(), "Datasets", "DatasetFiles", "countries_dataset.txt"));
                await _context.Countries.AddRangeAsync(countries);
            }
        }
    }
}
