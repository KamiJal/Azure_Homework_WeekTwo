using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KamiJal.WeatherForecast.Database
{
    public class WeatherForecastContext : DbContext
    {
        public DbSet<CityWeatherForecast> WeatherForecasts { get; set; }

        public WeatherForecastContext() : base("WeatherForecast") { }
        public WeatherForecastContext(string connectionString) : base(connectionString) { }
    }
}
