using Microsoft.EntityFrameworkCore;
using restAPI.Models;
using TelephoneNetworkApi.Repozitories;

namespace TelephoneNetworkApi.Persistence.Repositories
{
    public class SubscriberRepository : BaseRepository, ISubscriberRepositiry
    {
        public SubscriberRepository(AppDbContext context) : base(context)
        {

        }

        public async Task<IEnumerable<Subscriber>> ListAsync()
        {
            return await _context.Subscribers.ToListAsync();
        }
    }
}
