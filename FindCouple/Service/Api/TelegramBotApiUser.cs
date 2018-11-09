using System;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types;

namespace KamiJal.CoupleFinder.Api;
{
    internal class TelegramBotApiUser
    {
        private readonly ITelegramBotClient _botClient;
        private readonly string _infoMessage;

        public TelegramBotApiUser()
        {
            _botClient = new TelegramBotClient(Environment.GetEnvironmentVariable("TelegramBotApiKey"));
            _infoMessage = Environment.GetEnvironmentVariable("InfoMessageEn");
        }

        public void StartReceiving()
        {
            _botClient.OnMessage += Bot_OnMessage;
            _botClient.StartReceiving();
        }

        private async void Bot_OnMessage(object sender, MessageEventArgs e)
        {
            if (String.IsNullOrWhiteSpace(e.Message.Text)) return;

            switch (e.Message.Text)
            {
                case "/start":
                    {
                        await _botClient.SendTextMessageAsync(e.Message.Chat, $"Hello: {e.Message.Chat.FirstName}!");
                    }
                    break;

            }

            
        }
    }
}
