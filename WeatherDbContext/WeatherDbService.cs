using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KamiJal.WeatherForecast.Database;
using System.Configuration;

namespace WeatherDbContext
{
    public class WeatherDbService
    {
        private readonly WeatherForecastContext _context;

        public WeatherDbService()
        {
            _context = new WeatherForecastContext("Server=tcp:kj-server.database.windows.net,1433;Initial Catalog=WeatherForecast;Persist Security Info=False;User ID=kamijal;Password=meFer4ever;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
        }

        public async Task AddAsync(CityWeatherForecast item)
        {
            _context.WeatherForecasts.Add(item);
            await _context.SaveChangesAsync();
            _context.Dispose();
        }

        public async Task AddAsync(List<CityWeatherForecast> items)
        {
            _context.WeatherForecasts.AddRange(items);
            await _context.SaveChangesAsync();
            _context.Dispose();
        }
    }
}
