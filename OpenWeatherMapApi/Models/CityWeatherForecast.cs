using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace KamiJal.WeatherForecast.Models
{
    [Table("CityWeatherForecasts", Schema = "WeatherForecast")]
    public class CityWeatherForecast
    {
        public int Id { get; set; }
        [Required]
        public string CityName { get; set; }
        [Required]
        public string WeatherCondition { get; set; }
        [Required]
        public string Cloudiness { get; set; }
        [Required]
        public string Temperature { get; set; }
        [Required]
        public string Humidity { get; set; }
        [Required]
        public string Pressure { get; set; }
        [Required]
        public string WindSpeed { get; set; }
        [Required]
        public string WindDirection { get; set; }

        public CityWeatherForecast() { }

        public CityWeatherForecast(JToken token)
        {
            CityName = token["name"].ToString();
            WeatherCondition = token["weather"][0]["description"].ToString();
            Cloudiness = token["clouds"]["all"].ToString();
            Temperature = token["main"]["temp"].ToString();
            Humidity = token["main"]["humidity"].ToString();
            Pressure = token["main"]["pressure"].ToString();
            WindSpeed = token["wind"]["speed"].ToString();
            WindDirection = token["wind"]["deg"].ToString();
        }

        public override string ToString()
        {
            var divider = new string('-', 35) + Environment.NewLine;
            var builder = new StringBuilder();

            builder.Append($"{divider}{divider}{CityName.ToUpper()}\n{divider}");
            builder.Append($"\u2022 Weather condition: {WeatherCondition}\n");
            builder.Append($"\u2022 Cloudiness: {Cloudiness}%\n");
            builder.Append($"\u2022 Temperature: {Temperature} Celsius\n");
            builder.Append($"\u2022 Humidity: {Humidity}%\n");
            builder.Append($"\u2022 Pressure: {Pressure} hPa\n");
            builder.Append($"\u2022 Wind speed: {WindSpeed} meter/sec\n");
            builder.Append($"\u2022 Wind direction: {WindDirection} degrees (meteorological)\n");
            builder.Append($"{divider}{divider}\n");

            return builder.ToString();
        }

        public static string CombineWeatherReport(List<CityWeatherForecast> list)
        {
            if (!list?.Any() ?? true) throw new ArgumentNullException();

            var builder = new StringBuilder();
            foreach (var cityWeatherForecast in list)
            {
                builder.Append(cityWeatherForecast.ToString() + "\n");
            }

            return builder.ToString();
        }

    }
}
