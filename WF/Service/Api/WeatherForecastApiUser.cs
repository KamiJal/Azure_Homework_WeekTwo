using KamiJal.WeatherForecast.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using WeatherForecast.Models.Json;

namespace KamiJal.WeatherForecast.Service.Api
{
    internal class WeatherForecastApiUser
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;
        private readonly List<string> _cityIds;

        public WeatherForecastApiUser(string apiKey, List<string> cityIds)
        {
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri("http://api.openweathermap.org/data/2.5/")
            };

            _apiKey = apiKey;
            _cityIds = cityIds;
        }

        private async Task<string> GroupAsync() =>
            await _httpClient.GetStringAsync($"group?id={String.Join(",", _cityIds)}&units=metric&APPID={_apiKey}");

        public void Dispose() => _httpClient.Dispose();

        private OpenWeatherApiObject Parse(string json) => JsonConvert.DeserializeObject<OpenWeatherApiObject>(json);

        public async Task<List<CityWeatherForecast>> GetWeatherData() => 
            Parse(await GroupAsync()).list.Select(q => new CityWeatherForecast(q)).ToList();
    }
}
