using KamiJal.WeatherForecast.Service;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using WeatherForecast.Models.Json;

namespace KamiJal.WeatherForecast.Models
{
    [Table("CityWeatherForecasts")]
    public class CityWeatherForecast
    {
        public int Id { get; set; }
        public int Cloudiness { get; set; }
        public int Temperature { get; set; }
        public int Humidity { get; set; }
        public int Pressure { get; set; }
        public int WindSpeed { get; set; }
        public int WindDirection { get; set; }
        public DateTime RecordedDate { get; set; }

        [Required]
        public string CityName { get; set; }
        [Required]
        public string WeatherCondition { get; set; }

        public CityWeatherForecast() { }

        public CityWeatherForecast(CityList weatherForecast)
        {
            CityName = weatherForecast.name;
            WeatherCondition = weatherForecast.weather.First().description;
            Cloudiness = weatherForecast.clouds.all;
            Temperature = weatherForecast.main.temp;
            Humidity = weatherForecast.main.humidity;
            Pressure = weatherForecast.main.pressure;
            WindSpeed = weatherForecast.wind.speed;
            WindDirection = weatherForecast.wind.deg;
            RecordedDate = DateTime.Now;

            WeatherForecastService.Logger.Info($"Created city weather forecast with name: {CityName}");
        }

        public override string ToString()
        {
            var divider = new string('-', 39) + Environment.NewLine;
            var builder = new StringBuilder();

            builder.Append($"{divider}{divider}{CityName.ToUpper()}\n{divider}");
            builder.Append($"\u2022 Weather condition: {WeatherCondition}\n");
            builder.Append($"\u2022 Cloudiness: {Cloudiness}%\n");
            builder.Append($"\u2022 Temperature: {Temperature} Celsius\n");
            builder.Append($"\u2022 Humidity: {Humidity}%\n");
            builder.Append($"\u2022 Pressure: {Pressure} hPa\n");
            builder.Append($"\u2022 Wind speed: {WindSpeed} meter/sec\n");
            builder.Append($"\u2022 Wind direction: {WindDirection} degrees (meteorological)\n");
            builder.Append($"{divider}{divider}");

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
