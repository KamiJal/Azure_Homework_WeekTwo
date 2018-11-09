using System.IO;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using File = System.IO.File;

namespace KamiJal.WeatherForecast.Service.Api
{
    internal class TelegramBotApiUser
    {
        private readonly ITelegramBotClient _botClient;
        private readonly ChatId _channelId;
        
        public TelegramBotApiUser(string apiKey, long chatIdentifier)
        {
            _channelId = new ChatId(chatIdentifier);
            _botClient = new TelegramBotClient(apiKey);
        }

        public async Task SendTextMessageAsync(string message) => await _botClient.SendTextMessageAsync(_channelId, message);
    }
}
