using System;
using KamiJal.FindCouple.Service.Api;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;

namespace Kamijal.FindCouple.Service
{
    public static class FindCoupleService
    {
        private static readonly TelegramBotApiUser TelegramUser;

        static FindCoupleService()
        {
            TelegramUser = new TelegramBotApiUser();
        }

        [FunctionName("FindCoupleService")]
        public static void Run([TimerTrigger("* * * * * *")]TimerInfo myTimer, TraceWriter log)
        {
            log.Info($"C# Timer trigger function executed at: {DateTime.Now}");
            TelegramUser.StartReceiving();
            while (true)
            {
                
            }

        }
    }
}
