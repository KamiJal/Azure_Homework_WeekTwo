using KamiJal.WeatherForecast.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KamiJal.WeatherForecast.Service
{
    public class WeatherDbService
    {
        private readonly WeatherForecastContext _context;

        public WeatherDbService() => _context = new WeatherForecastContext();
        public void Dispose() => _context.Dispose();

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
