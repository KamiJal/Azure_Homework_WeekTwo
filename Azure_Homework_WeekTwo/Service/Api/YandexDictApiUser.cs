using KamiJal.WordSynonyms.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace KamiJal.WordSynonyms.Service.Api
{
    public class YandexDictApiUser
    {
        private readonly HttpClient _httpClient;
        private readonly string _text;
        private readonly string _apiKey;

        private const string Ru = "ru-ru";
        private const string En = "en-en";

        public YandexDictApiUser(string text, string apiKey)
        {
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri("https://dictionary.yandex.net/api/v1/dicservice.json/")
            };

            _text = text;
            _apiKey = apiKey;
        }

        private async Task<string> LookupAsync(string lang) => 
            await _httpClient.GetStringAsync($"lookup?key={_apiKey}&lang={lang}&text={_text}");
        public void Dispose() => _httpClient.Dispose();

        //JSON parsing
        private YandexDictApiObject Parse(string json) => JsonConvert.DeserializeObject<YandexDictApiObject>(json);
        private string Translation(Translation translation) => translation.text;
        private List<string> Synonyms(Translation translation) => translation.syn?.Select(synonym => synonym.text).ToList();

        private string GenerateMessage(YandexDictApiObject yandexObject)
        {
            var dictionary = yandexObject.def.Single().tr.ToDictionary(Translation, Synonyms);

            var divider = new string('-', 35) + Environment.NewLine;
            var builder = new StringBuilder($"{divider}{divider}{_text.ToUpper()}\n{divider}{divider}");

            foreach (var pair in dictionary)
            {
                builder.Append($"\n{pair.Key.ToUpper()}\n");
                if (pair.Value != null)
                {
                    foreach (var meaning in pair.Value)
                    {
                        builder.Append($"\u2022 {meaning}\n");
                    }
                }
                builder.Append($"\n{divider}");
            }

            return builder.ToString();
        }

        public async Task<string> GetSynonymsAsync()
        {
            var yandexObject = Parse(await LookupAsync(En));
            if (yandexObject.def.Any()) return GenerateMessage(yandexObject);

            yandexObject = Parse(await LookupAsync(Ru));
            return !yandexObject.def.Any() ? "No data found by this query!" : GenerateMessage(yandexObject);
        }
    }
}
