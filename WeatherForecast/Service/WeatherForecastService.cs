using System;
using System.Collections.Generic;
using KamiJal.WeatherForecast.Models;
using KamiJal.WeatherForecast.Service.Api;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;

namespace KamiJal.WeatherForecast.Service
{
    public static class WeatherForecastService
    {
        private const string Almaty = "1526395";
        private const string Astana = "1538317";
        private const string Shymkent = "1518980";

        public static TraceWriter Logger;

        [FunctionName("WeatherForecastService")]
        public static async void Run([TimerTrigger("0 */5 * * * *")] TimerInfo myTimer, TraceWriter log)
        {
            Logger = log;
            log.Info($"C# Timer trigger function executed at: {DateTime.Now}");

            WeatherDbService weatherDbService = null;
            WeatherForecastApiUser weatherApiUser = null;

            try
            {
                var openWeatherApiKey = Environment.GetEnvironmentVariable("OpenWeatherMapApiKey");
                var telegramBotApiKey = Environment.GetEnvironmentVariable("TelegramBotApiKey");
                var telegramChannelId = Convert.ToInt64(Environment.GetEnvironmentVariable("TelegramChannelId"));

                weatherDbService = new WeatherDbService();
                weatherApiUser =
                    new WeatherForecastApiUser(openWeatherApiKey, new List<string> {Almaty, Astana, Shymkent});

                var cityWeatherForecasts = await weatherApiUser.GetWeatherData();
                await weatherDbService.AddAsync(cityWeatherForecasts);

                var telegramBotApiUser = new TelegramBotApiUser(telegramBotApiKey, telegramChannelId);
                await telegramBotApiUser.SendTextMessageAsync(
                    CityWeatherForecast.CombineWeatherReport(cityWeatherForecasts));
            }
            catch (Exception ex)
            {
                log.Error(ex.Message, ex);
            }
            finally
            {
                weatherApiUser?.Dispose();
                weatherDbService?.Dispose();
            }
        }
    }
}