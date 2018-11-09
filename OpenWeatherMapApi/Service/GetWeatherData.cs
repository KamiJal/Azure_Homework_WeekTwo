using KamiJal.WeatherForecast.Models;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using System;
using System.Collections.Generic;

namespace KamiJal.WeatherForecast.Service
{
    public static class GetWeatherData
    {
        private const string Almaty = "1526395";
        private const string Astana = "1538317";
        private const string Shymkent = "1518980";
        
        [FunctionName("GetWeatherData")]
        public static async void Run([TimerTrigger("0 */2 * * * *")]TimerInfo myTimer, TraceWriter log)
        {
            log.Info($"C# Timer trigger function executed at: {DateTime.Now}");

            var myDocs = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            var weatherApiKeyFilepath = myDocs + @"\WEATHER_MAP_API.txt";
            var tBotApiKeyFilepath = myDocs + @"\KJ_WEATHERFORECAST_TELEGRAM.txt";

            var weatherApiUser = new WeatherForecastApiUser(weatherApiKeyFilepath, new List<string> { Almaty, Astana, Shymkent });

            List<CityWeatherForecast> weatherCityList = null;
            try
            {
                weatherCityList = await weatherApiUser.GetWeatherData();
                weatherApiUser.Dispose();
            }
            catch (Exception ex)
            {
                log.Error(ex.Message, ex);
            }

            var weatherDbService = new DbContextService();
            await weatherDbService.AddAsync(weatherCityList);


            var telegramBotApiUser= new TelegramBotApiUser(tBotApiKeyFilepath, -1001155437894);
            await telegramBotApiUser.SendTextMessageAsync(CityWeatherForecast.CombineWeatherReport(weatherCityList));
        }
    }
}
