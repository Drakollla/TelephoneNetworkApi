using TelephoneNetworkApi.Domain.Models;

namespace TelephoneNetworkApi.Domain.Repositories
{
    public interface IRegistrySubscriptionPaymentRepository
    {
        Task<IEnumerable<RegistrySubscriptionPayment>> ListAsync();
        Task AddAsync(RegistrySubscriptionPayment registrySubscriptionPayment);
        Task<RegistrySubscriptionPayment> FindByIdAsync(int id);
        void Update(RegistrySubscriptionPayment registrySubscriptionPayment);
        void Remove(RegistrySubscriptionPayment registrySubscriptionPayment);
    }
}