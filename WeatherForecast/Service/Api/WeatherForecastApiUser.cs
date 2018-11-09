using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using KamiJal.WeatherForecast.Models;
using Newtonsoft.Json;
using WeatherForecast.Models.Json;

namespace KamiJal.WeatherForecast.Service.Api
{
    internal class WeatherForecastApiUser
    {
        private readonly string _apiKey;
        private readonly List<string> _cityIds;
        private readonly HttpClient _httpClient;

        public WeatherForecastApiUser(string apiKey, List<string> cityIds)
        {
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri("http://api.openweathermap.org/data/2.5/")
            };

            _apiKey = apiKey;
            _cityIds = cityIds;
        }

        private async Task<string> GroupAsync()
        {
            return await _httpClient.GetStringAsync(
                $"group?id={string.Join(",", _cityIds)}&units=metric&APPID={_apiKey}");
        }

        public void Dispose()
        {
            _httpClient.Dispose();
        }

        private OpenWeatherApiObject Parse(string json)
        {
            return JsonConvert.DeserializeObject<OpenWeatherApiObject>(json);
        }

        public async Task<List<CityWeatherForecast>> GetWeatherData()
        {
            return Parse(await GroupAsync()).list.Select(q => new CityWeatherForecast(q)).ToList();
        }
    }
}