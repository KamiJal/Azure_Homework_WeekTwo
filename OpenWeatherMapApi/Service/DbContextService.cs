using KamiJal.WeatherForecast.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KamiJal.WeatherForecast.Service
{
    public class DbContextService
    {
        private readonly WeatherForecastContext _context;

        public DbContextService() => _context = new WeatherForecastContext();

        public async Task AddAsync(CityWeatherForecast item)
        {
            _context.CityWeatherForecasts.Add(item);
            await _context.SaveChangesAsync();
            _context.Dispose();
        }

        public async Task AddAsync(List<CityWeatherForecast> items)
        {
            _context.CityWeatherForecasts.AddRange(items);
            await _context.SaveChangesAsync();
            _context.Dispose();
        }
    }
}
