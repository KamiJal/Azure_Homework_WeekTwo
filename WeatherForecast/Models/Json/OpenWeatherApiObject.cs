using System.Collections.Generic;

namespace WeatherForecast.Models.Json
{
    public class OpenWeatherApiObject
    {
        public int cnt { get; set; }
        public List<CityList> list { get; set; }
    }
}