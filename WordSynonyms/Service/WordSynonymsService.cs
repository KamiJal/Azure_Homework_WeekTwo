using System;
using System.Threading.Tasks;
using KamiJal.WordSynonyms.Service.Api;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;

namespace KamiJal.WordSynonyms.Service
{
    public static class WordSynonymsService
    {
        [FunctionName("WordSynonymsService")]
        public static async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Anonymous, "get")]
            HttpRequest req, ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            string text = req.Query["text"];
            if (string.IsNullOrWhiteSpace(text))
                return new BadRequestObjectResult(
                    @"Please pass a text on the query string in format: ""?text=some_text""");
            ;

            IActionResult result = new StatusCodeResult(StatusCodes.Status500InternalServerError);
            YandexDictApiUser yandexDictApiUser = null;

            try
            {
                var yandexDictApiKey = Environment.GetEnvironmentVariable("YandexDictApiKey");
                yandexDictApiUser = new YandexDictApiUser(text, yandexDictApiKey);

                var synonyms = await yandexDictApiUser.GetSynonymsAsync();
                result = new OkObjectResult(synonyms);
            }
            catch (Exception ex)
            {
                log.LogError(ex.Message, ex);
            }
            finally
            {
                yandexDictApiUser?.Dispose();
            }

            return result;
        }
    }
}