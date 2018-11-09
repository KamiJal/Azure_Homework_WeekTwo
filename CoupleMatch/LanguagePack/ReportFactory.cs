namespace KamiJal.CoupleMatch.LanguagePack
{
    public static class ReportFactory
    {
        public static Report Create(string languageCode)
        {
            switch (languageCode)
            {
                case "ru":
                {
                    return new ReportRu();
                }
                default:
                {
                    return new ReportEn();
                }
            }
        }
    }
}