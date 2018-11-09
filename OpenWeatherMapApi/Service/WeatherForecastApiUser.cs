using KamiJal.WeatherForecast.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace KamiJal.WeatherForecast.Service
{
    internal class WeatherForecastApiUser
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKeyKeyFilepath;
        private readonly List<string> _cityIds;

        public WeatherForecastApiUser(string apiKeyFilepath, List<string> cityIds)
        {
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri("http://api.openweathermap.org/data/2.5/")
            };

            _apiKeyKeyFilepath = apiKeyFilepath;
            _cityIds = cityIds;
        }

        private string ReadApiKey() => File.Exists(_apiKeyKeyFilepath) ? File.ReadAllText(_apiKeyKeyFilepath) : 
            throw new FileNotFoundException();

        private async Task<string> GroupAsync() =>
            await _httpClient.GetStringAsync($"group?id={String.Join(",", _cityIds)}&units=metric&APPID={ReadApiKey()}");

        public void Dispose() => _httpClient.Dispose();

        public async Task<List<CityWeatherForecast>> GetWeatherData()
        {
            var responseJson = await GroupAsync();
            var parsed = JsonConvert.DeserializeObject<JObject>(responseJson)["list"].ToList();
            var generatedList = new List<CityWeatherForecast>();

            foreach (var token in parsed)
            {
                var cityWeatherForecast = new CityWeatherForecast(token);
                generatedList.Add(cityWeatherForecast);
            }

            return generatedList;
        }
    }
}
