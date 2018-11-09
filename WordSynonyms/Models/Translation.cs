using System.Collections.Generic;

namespace KamiJal.WordSynonyms.Models
{
    public class Translation
    {
        public string text { get; set; }
        public string pos { get; set; }
        public List<Synonym> syn { get; set; }
    }
}