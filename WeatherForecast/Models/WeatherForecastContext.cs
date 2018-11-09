using System.Data.Entity;

namespace KamiJal.WeatherForecast.Models
{
    public class WeatherForecastContext : DbContext
    {
        public virtual DbSet<CityWeatherForecast> CityWeatherForecasts { get; set; }
    }
}