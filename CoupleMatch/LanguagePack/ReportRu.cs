using System.Text;
using KamiJal.CoupleMatch.Models;

namespace KamiJal.CoupleMatch.LanguagePack
{
    public sealed class ReportRu : Report
    {
        public ReportRu()
        {
            Usage();
        }

        public override void Usage()
        {
            Text = "Использование бота:\n" +
                   "1. /register - для регистрации в системе\n" +
                   "2. отправьте свой портрет в хорошем разрешении как медиафайл\n" +
                   "3. /findCouple - получить данные подходящих людей\n";
        }

        public override void Hello(string name)
        {
            Text = $"Приветствую, {name}";
        }

        public override void SuccessfullyRegistered(string name)
        {
            Text = $"{name}, вы успешно зарегистрированы!";
        }

        public override void AlreadyRegistered(string name)
        {
            Text = $"{name}, вы уже зарегистрированы!";
        }

        public override void NotRegistered()
        {
            Text = "Вы не зарегистрированы! Отправьте /register для регистрации.";
        }

        public override void InternalError()
        {
            Text = "Произошла внутренняя ошибка. Попробуйте еще раз.";
        }

        public override void PhotoError()
        {
            Text = "Пожалуйста пришлите фото хорошего качества, где вы одни на снимке!";
        }

        public override void PhotoRegistered(Subscriber subscriber)
        {
            var sb = new StringBuilder();

            sb.Append("Ваше фото зарегистрировано!\n");
            sb.Append($"Пол: {(subscriber.Gender.Equals("Male") ? "мужской" : "женский")}\n");
            sb.Append($"Возраст: {subscriber.Age}");

            Text = sb.ToString();
        }

        public override string GeneratePhotoCaption(Subscriber subscriber)
        {
            var sb = new StringBuilder();
            sb.Append($"Имя: {subscriber.FirstName} {subscriber.LastName}\n");
            sb.Append($"Возраст: {subscriber.Age}");

            return sb.ToString();
        }

        public override void PhotoNotRegistered()
        {
            Text = "Пожалуйста отправьте сначала свое фото!";
        }

        public override void PhotoAlreadyRegistered()
        {
            Text = "Вы уже зарегистрировали фотографию!";
        }

        public override void NoMatchesFound()
        {
            Text = "К сожалению, подходящих пар не найдено!";
        }

        public override void MatchesFound()
        {
            Text = "Найдены подходящие варианты!";
        }
    }
}