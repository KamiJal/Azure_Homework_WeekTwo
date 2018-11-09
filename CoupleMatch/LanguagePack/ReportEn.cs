using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KamiJal.CoupleMatch.Models;

namespace KamiJal.CoupleMatch.LanguagePack
{
    public sealed class ReportEn : Report
    {
        public ReportEn() => Usage();

        public override void Usage() =>
            Text = "To use bot send:\n" +
                   "1. /register - to register yourself in system\n" +
                   "2. send your photo portrait in good resolution as media\n" +
                   "3. /findCouple - to receive matching people info";

        public override void Hello(string name) => Text = $"Hello, {name}";

        public override void SuccessfullyRegistered(string name) => Text = $"{name}, you are successfully registered!";
        public override void AlreadyRegistered(string name) => Text = $"{name}, you are already registered!";
        public override void NotRegistered() => Text = "You are not registered! Send /register to register yourself.";


        public override void InternalError() => Text = "Internal error occured. Please try again.";
        public override void PhotoError() => Text = "Please send photo in good quality where you are alone!";

        public override void PhotoRegistered(Subscriber subscriber)
        {
            var sb = new StringBuilder();

            sb.Append("You photo has been registered!\n");
            sb.Append($"Gender: { (subscriber.Gender.Equals("Male") ? "male" : "female") }\n");
            sb.Append($"Age: {subscriber.Age}");

            Text = sb.ToString();
        }

        public override string GeneratePhotoCaption(Subscriber subscriber)
        {
            var sb = new StringBuilder();

            sb.Append($"Name: {subscriber.FirstName} {subscriber.LastName}\n");
            sb.Append($"Age: {subscriber.Age}");

            return sb.ToString();
        }

        public override void PhotoNotRegistered() => Text = "Please send photo before searching couple!";
        public override void PhotoAlreadyRegistered() => Text = "You have already registered your photo!";
        public override void NoMatchesFound() => Text = "Unfortunately, no matching couple was found!";
        public override void MatchesFound() => Text = "Matching people were found!";
    }
}
