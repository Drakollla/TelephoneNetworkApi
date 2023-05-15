using Microsoft.EntityFrameworkCore;
using TelephoneNetworkApi.Domain.Models;
using TelephoneNetworkApi.Domain.Repositories;

namespace TelephoneNetworkApi.Persistence.Repositories
{
    public class SubscriberRepository : BaseRepository, ISubscriberRepository
    {
        public SubscriberRepository(AppDbContext context) : base(context) { }

        public async Task<IEnumerable<Subscriber>> ListAsync()
        {
            return await _context.Subscribers.Include(x => x.AtsSubscribers)
                .ThenInclude(e => e.AutomaticTelephoneExchange)
                .Include(x => x.RegistrySubscriptionPayments)
                .ToListAsync();
        }

        public async Task AddAsync(Subscriber subscriber)
        {
            await _context.Subscribers.AddAsync(subscriber);
        }

        public async Task<Subscriber> FindByIdAsync(int id)
        {
            return await _context.Subscribers.FindAsync(id);
        }

        public void Update(Subscriber subscriber)
        {
            _context.Subscribers.Update(subscriber);
        }

        public void Remove(Subscriber subscriber)
        {
            _context.Subscribers.Remove(subscriber);
        }
    }
}