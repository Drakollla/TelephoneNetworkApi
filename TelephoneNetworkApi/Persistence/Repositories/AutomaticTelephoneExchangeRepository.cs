using Microsoft.EntityFrameworkCore;
using TelephoneNetworkApi.Domain.Models;
using TelephoneNetworkApi.Domain.Repositories;

namespace TelephoneNetworkApi.Persistence.Repositories
{
    public class AutomaticTelephoneExchangeRepository : BaseRepository, IAutomaticTelephoneExchangeRepository
    {
        public AutomaticTelephoneExchangeRepository(AppDbContext context) : base(context) { }

        public async Task AddAsync(AutomaticTelephoneExchange automaticTelephoneExchange)
        {
            await _context.AutomaticTelephoneExchanges.AddAsync(automaticTelephoneExchange);
        }

        public async Task<IEnumerable<AutomaticTelephoneExchange>> ListAsync()
        {
            return await _context.AutomaticTelephoneExchanges.ToListAsync();
        }

        public async Task<AutomaticTelephoneExchange> FindByIdAsync(int id)
        {
            return await _context.AutomaticTelephoneExchanges.FindAsync(id);
        }

        public void Update(AutomaticTelephoneExchange automaticTelephoneExchange)
        {
            _context.AutomaticTelephoneExchanges.Update(automaticTelephoneExchange);
        }

        public void Remove(AutomaticTelephoneExchange automaticTelephoneExchange)
        {
            _context.AutomaticTelephoneExchanges.Remove(automaticTelephoneExchange);
        }
    }
}