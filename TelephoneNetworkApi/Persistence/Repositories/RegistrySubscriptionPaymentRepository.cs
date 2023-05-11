using Microsoft.EntityFrameworkCore;
using TelephoneNetworkApi.Domain.Models;
using TelephoneNetworkApi.Domain.Repositories;

namespace TelephoneNetworkApi.Persistence.Repositories
{
    public class RegistrySubscriptionPaymentRepository : BaseRepository, IRegistrySubscriptionPaymentRepository
    {
        public RegistrySubscriptionPaymentRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<RegistrySubscriptionPayment>> ListAsync()
        {
            return await _context.RegistrySubscriptionPayments.Include(p => p.Subscriber)
                .ToListAsync();
        }

        public async Task AddAsync(RegistrySubscriptionPayment registrySubscriptionPayment)
        {
            await _context.RegistrySubscriptionPayments.AddAsync(registrySubscriptionPayment);
        }

        public async Task<RegistrySubscriptionPayment> FindByIdAsync(int id)
        {
            return await _context.RegistrySubscriptionPayments.Include(r => r.Subscriber)
                .FirstOrDefaultAsync(r => r.Id == id);
        }

        public void Update(RegistrySubscriptionPayment registrySubscriptionPayment)
        {
            _context.RegistrySubscriptionPayments.Update(registrySubscriptionPayment);
        }

        public void Remove(RegistrySubscriptionPayment registrySubscriptionPayment)
        {
            _context.RegistrySubscriptionPayments.Remove(registrySubscriptionPayment);
        }
    }
}
