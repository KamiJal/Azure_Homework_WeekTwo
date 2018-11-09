using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using KamiJal.CoupleMatch.Api;
using KamiJal.CoupleMatch.LanguagePack;
using KamiJal.CoupleMatch.Models;
using Microsoft.Azure.CognitiveServices.Vision.Face.Models;
using Telegram.Bot.Args;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using File = Telegram.Bot.Types.File;

namespace KamiJal.CoupleMatch.Service
{
    public class CoupleMatchManager
    {
        private readonly BlobManager _blobManager;
        private readonly ContextManager _contextManager;
        private readonly FaceApiUser _faceApi;
        private readonly TelegramBotApiUser _telegramBot;
        private Subscriber _currentSubscriber;
        private List<Subscriber> _matchingSubscribers;
        private Message _message;

        private Report _report;

        public CoupleMatchManager()
        {
            _telegramBot = new TelegramBotApiUser();
            _contextManager = new ContextManager();
            _blobManager = new BlobManager();
            _faceApi = new FaceApiUser();
        }

        public void Start()
        {
            _telegramBot.StartReceiving(Bot_OnMessage);
        }

        public void End()
        {
            _telegramBot.StopReceiving();
        }


        private async void Bot_OnMessage(object sender, MessageEventArgs e)
        {
            RegisterInnerVariables(e.Message);

            switch (e.Message.Type)
            {
                case MessageType.PhotoMessage:
                    await RegisterPhotoAsync();
                    break;
                case MessageType.TextMessage:
                    await TextMessageLogic(e.Message.Text);
                    break;
            }

            await _telegramBot.SendTextMessageAsync(e.Message.Chat, _report.Text);

            if (_matchingSubscribers != null)
            {
                await SendMatchingCouplesAsync();
                _matchingSubscribers = null;
            }
        }

        private async Task SendMatchingCouplesAsync()
        {
            foreach (var subscriber in _matchingSubscribers)
            {
                var photoUrl = await _blobManager.GetPhotoUrlByFilename($"{subscriber.FileId}.jpg");
                var caption = _report.GeneratePhotoCaption(subscriber);

                await _telegramBot.SendPhotoAsync(_message.Chat, photoUrl, caption);
            }
        }

        private void RegisterInnerVariables(Message message)
        {
            _message = message;
            _report = ReportFactory.Create(_message.From.LanguageCode);
            _currentSubscriber = new Subscriber(message.Chat);
        }

        private async Task TextMessageLogic(string text)
        {
            switch (text ?? string.Empty)
            {
                case "/register":
                    await RegisterAsync();
                    break;
                case "/findCouple":
                    await FindCouple();
                    break;
            }
        }

        private async Task FindCouple()
        {
            if (!_contextManager.IsRegistered(_currentSubscriber))
            {
                _report.NotRegistered();
                return;
            }

            var subscriberInDb = await _contextManager.GetSubscriberByChatIdAsync(_message.Chat.Id);

            if (!subscriberInDb.PhotoProvided)
            {
                _report.PhotoNotRegistered();
                return;
            }

            _matchingSubscribers = await _contextManager.GetMatchingPeople(subscriberInDb, 5);

            if (!_matchingSubscribers.Any())
            {
                _report.NoMatchesFound();
                _matchingSubscribers = null;
                return;
            }

            _report.MatchesFound();
        }

        private async Task RegisterPhotoAsync()
        {
            try
            {
                if (!_contextManager.IsRegistered(_currentSubscriber))
                {
                    _report.NotRegistered();
                    return;
                }

                var subscriberInDb = await _contextManager.GetSubscriberByChatIdAsync(_message.Chat.Id);

                var photoFile = await _telegramBot.GetFileAsync(_message.Photo);
                var buffer = await GetByteArrayFromFile(photoFile);

                var photoUrl = await _blobManager.UploadFileAsync(photoFile.FileId + ".jpg", buffer);
                var detectedFace = await _faceApi.MakeAnalysisRequest(photoUrl);

                if (subscriberInDb.PhotoProvided) await _blobManager.DeleteFileAsync(subscriberInDb.FileId + ".jpg");

                subscriberInDb.FullFill(detectedFace, photoFile.FileId, _message.Photo);

                if (!await _contextManager.SaveChangesSafeAsync()) throw new Exception();

                _report.PhotoRegistered(subscriberInDb);
            }
            catch (APIErrorException)
            {
                _report.PhotoError();
            }
            catch (Exception)
            {
                _report.InternalError();
            }
        }


        private async Task<byte[]> GetByteArrayFromFile(File file)
        {
            using (var input = new MemoryStream())
            {
                await file.FileStream.CopyToAsync(input);
                return input.ToArray();
            }
        }

        private async Task RegisterAsync()
        {
            if (_contextManager.IsRegistered(_currentSubscriber))
            {
                _report.AlreadyRegistered(_currentSubscriber.FirstName);
                return;
            }


            if (await _contextManager.RegisterSubscriberAsync(_currentSubscriber))
            {
                _report.SuccessfullyRegistered(_currentSubscriber.FirstName);
                return;
            }

            _report.InternalError();
        }
    }
}