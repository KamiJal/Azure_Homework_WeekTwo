using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherForecast.Models.Json
{
    public class OpenWeatherApiObject
    {
        public int cnt { get; set; }
        public List<CityList> list { get; set; }
    }
}
