namespace MusicFestival.Data.Datasets
{
    public class DataSeeder(AppDbContext context)
    {
        private readonly AppDbContext _context = context;

        public async Task SeedAsync()
        {
            var solutionRoot = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, @"..\..\..\..\"));
            bool hasChanges = false;

            if (!_context.Genres.Any())
            {
                var genres = DatasetHandler.GetGenreData(Path.Combine(solutionRoot, "Festival.Data", "Datasets", "DatasetFiles", "genres_dataset.txt"));
                await _context.Genres.AddRangeAsync(genres);
                hasChanges = true;
            }

            if (!_context.Countries.Any())
            {
                var countries = DatasetHandler.GetCountryData(Path.Combine(solutionRoot, "Festival.Data", "Datasets", "DatasetFiles", "countries_dataset.txt"));
                await _context.Countries.AddRangeAsync(countries);
                hasChanges = true;
            }

            if (hasChanges)
            {
                await _context.SaveChangesAsync();
            }
        }
    }
}
