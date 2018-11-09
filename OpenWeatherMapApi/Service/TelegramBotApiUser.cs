using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using File = System.IO.File;

namespace KamiJal.WeatherForecast.Service
{
    internal class TelegramBotApiUser
    {
        private readonly ITelegramBotClient _botClient;
        private readonly ChatId _channelId;
        
        public TelegramBotApiUser(string apiKeyFilepath, long chatIdentifier)
        {
            _channelId = new ChatId(chatIdentifier);
            _botClient = new TelegramBotClient(ReadApiKey(apiKeyFilepath));
        }

        private static string ReadApiKey(string filePath) => System.IO.File.Exists(filePath) ? File.ReadAllText(filePath) : throw new FileNotFoundException();

        public async Task SendTextMessageAsync(string message) => await _botClient.SendTextMessageAsync(_channelId, message);
    }
}
