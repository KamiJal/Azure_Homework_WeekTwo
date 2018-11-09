using System.Collections.Generic;

namespace KamiJal.WordSynonyms.Models
{
    public class YandexDictApiObject
    {
        public Header head { get; set; }
        public List<Definition> def { get; set; }
    }
}