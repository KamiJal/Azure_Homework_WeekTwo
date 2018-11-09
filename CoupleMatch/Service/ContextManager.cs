using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KamiJal.CoupleMatch.Models;

namespace KamiJal.CoupleMatch.Service
{
    public class ContextManager
    {
        private readonly CoupleMatchContext _context;

        public ContextManager()
        {
            _context = new CoupleMatchContext();
        }

        public bool IsRegistered(Subscriber subscriber) => _context.Subscribers.Any(q => q.ChatId == subscriber.ChatId);

        public async Task<bool> RegisterSubscriberAsync(Subscriber subscriber)
        {
            _context.Subscribers.Add(subscriber);
            return await SaveChangesSafeAsync();
        }

        public async Task<Subscriber> GetSubscriberByChatIdAsync(long chatId) =>
            await Task.Run(() => { return _context.Subscribers.Single(q => q.ChatId == chatId); });

        public async Task<List<Subscriber>> GetMatchingPeople(Subscriber subscriber, int ageOffset) => 
            await Task.Run(() =>
            {
                return _context.Subscribers
                    .Where(q => q.PhotoProvided && !q.Gender.Equals(subscriber.Gender) && 
                                (q.Age > subscriber.Age - ageOffset && q.Age < subscriber.Age + ageOffset)).ToList();
            });

        public async Task<bool> SaveChangesSafeAsync()
        {
            try
            {
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
