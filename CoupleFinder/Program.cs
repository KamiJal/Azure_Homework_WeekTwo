using KamiJal.CoupleFinder.Api;
using System;

namespace KamiJal.CoupleFinder
{
    class Program
    {
        private static readonly TelegramBotApiUser TelegramUser;

        static Program()
        {
            TelegramUser = new TelegramBotApiUser();
        }

        static void Main(string[] args)
        {
            TelegramUser.StartReceiving();

            Console.ReadLine();
        }
    }
}
