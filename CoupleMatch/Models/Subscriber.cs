using System;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.Azure.CognitiveServices.Vision.Face.Models;
using Newtonsoft.Json;
using Telegram.Bot.Types;

namespace KamiJal.CoupleMatch.Models
{
    [Table("Subscribers")]
    public class Subscriber
    {
        public int Id { get; set; }

        public long ChatId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Gender { get; set; }

        public int Age { get; set; }

        public string FileId { get; set; }

        public string PhotoSizeJson { get; set; }

        public bool PhotoProvided { get; set; }

        public DateTime RegisteredDate { get; set; }

        public Subscriber() { }
        public Subscriber(Chat chat)
        {
            ChatId = chat.Id;
            FirstName = chat.FirstName;
            LastName = chat.LastName;
            RegisteredDate = DateTime.Now;
        }

        public bool FullFill(DetectedFace face, string fileId, PhotoSize[] photoSize)
        {

            if (!face?.FaceAttributes?.Age.HasValue ?? true) return false;
            if (!face?.FaceAttributes?.Gender.HasValue ?? true) return false;

            Age = (int)face.FaceAttributes.Age.Value;
            Gender = face.FaceAttributes.Gender.Value.ToString();
            FileId = fileId;
            PhotoSizeJson = JsonConvert.SerializeObject(photoSize);
            PhotoProvided = true;

            return true;
        }
    }
}
