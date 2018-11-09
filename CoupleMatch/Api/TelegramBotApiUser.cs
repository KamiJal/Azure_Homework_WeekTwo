using System;
using System.IO;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types;
using File = Telegram.Bot.Types.File;

namespace KamiJal.CoupleMatch.Api
{
    internal class TelegramBotApiUser
    {
        private readonly ITelegramBotClient _botClient;

        public TelegramBotApiUser()
        {
            _botClient = new TelegramBotClient("{YOUR_TELEGRAM_BOT_TOKEN}");
        }

        public void StartReceiving(EventHandler<MessageEventArgs> handler)
        {
            _botClient.OnMessage += handler;
            _botClient.StartReceiving();
        }

        public void StopReceiving()
        {
            _botClient.StopReceiving();
        }

        public async Task SendTextMessageAsync(ChatId chatId, string message)
        {
            await _botClient.SendTextMessageAsync(chatId, message);
        }

        public async Task SendPhotoAsync(ChatId chatId, string url, string caption)
        {
            await _botClient.SendPhotoAsync(chatId, new FileToSend(new Uri(url)), caption);
        }

        public async Task SendPhotoAsync(ChatId chatId, Stream content, string caption)
        {
            await _botClient.SendPhotoAsync(chatId, new FileToSend("photo.jpg", content), caption);
        }

        public async Task<File> GetFileAsync(PhotoSize[] photo)
        {
            return await _botClient.GetFileAsync(photo[photo.Length - 1].FileId);
        }
    }
}