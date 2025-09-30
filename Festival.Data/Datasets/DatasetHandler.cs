using MusicFestival.Core.Entities;
using System.Text.RegularExpressions;

namespace MusicFestival.Data.Datasets
{
    public static class DatasetHandler
    {
        public static IEnumerable<Genre> GetGenreData(string filePath)
        {
            var genres = new List<Genre>();
            var lines = File.ReadAllLines(filePath);

            int id = 1;

            foreach (var line in lines)
            {
                genres.Add(new Genre { Id = id++, Name = line.Trim() });
            }

            return genres;
        }

        public static IEnumerable<Country> GetCountryData(string filePath)
        {
            var countries = new List<Country>();
            var lines = File.ReadAllLines(filePath);
            var regex = new Regex(@"^(.*?)\s\([A-Z]{2}\)$");

            int id = 1;

            foreach (var line in lines)
            {
                var match = regex.Match(line);
                if (match.Success)
                {
                    countries.Add(new Country { Id = id++, Name = match.Groups[1].Value });
                }
            }

            return countries;
        }
    }
}
