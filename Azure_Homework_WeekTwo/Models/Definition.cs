using System.Collections.Generic;

namespace KamiJal.WordSynonyms.Models
{
    public class Definition
    {
        public string text { get; set; }
        public string pos { get; set; }
        public string ts { get; set; }
        public string fl { get; set; }
        public List<Translation> tr { get; set; }
    }
}
