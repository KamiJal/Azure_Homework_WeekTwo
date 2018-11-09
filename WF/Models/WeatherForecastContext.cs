using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KamiJal.WeatherForecast.Models
{
    public class WeatherForecastContext : DbContext
    {
        public virtual DbSet<CityWeatherForecast> CityWeatherForecasts { get; set; }
    }
}
