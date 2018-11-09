using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherForecast.Models.Json
{
    public class CityList
    {
        public Coordinates coord { get; set; }
        public InternalInfo sys { get; set; }
        public List<Weather> weather { get; set; }
        public MainInfo main { get; set; }
        public int visibility { get; set; }
        public Wind wind { get; set; }
        public Clouds clouds { get; set; }
        public int dt { get; set; }
        public int id { get; set; }
        public string name { get; set; }
    }
}
