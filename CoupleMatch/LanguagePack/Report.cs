using KamiJal.CoupleMatch.Models;

namespace KamiJal.CoupleMatch.LanguagePack
{
    public abstract class Report
    {
        public string Text { get; protected set; }

        public abstract void Usage();

        public abstract void AlreadyRegistered(string name);
        public abstract void SuccessfullyRegistered(string name);
        public abstract void NotRegistered();
        public abstract void PhotoRegistered(Subscriber subscriber);
        public abstract void PhotoNotRegistered();
        public abstract void PhotoAlreadyRegistered();

        public abstract void Hello(string name);

        public abstract void InternalError();
        public abstract void PhotoError();

        public abstract void NoMatchesFound();
        public abstract void MatchesFound();
        public abstract string GeneratePhotoCaption(Subscriber subscriber);
    }
}