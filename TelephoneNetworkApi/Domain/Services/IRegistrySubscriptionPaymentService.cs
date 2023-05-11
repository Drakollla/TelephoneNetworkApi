using TelephoneNetworkApi.Domain.Models;
using TelephoneNetworkApi.Domain.Services.Communication;

namespace TelephoneNetworkApi.Domain.Services
{
    public interface IRegistrySubscriptionPaymentService
    {
        Task<IEnumerable<RegistrySubscriptionPayment>> ListAsync();
        Task<RegistrySubscriptionPaymentResponse> SaveAsync(RegistrySubscriptionPayment registrySubscriptionPayment);
        Task<RegistrySubscriptionPaymentResponse> UpdateAsync(int id, RegistrySubscriptionPayment registrySubscriptionPayment);
        Task<RegistrySubscriptionPaymentResponse> DeleteAsync(int id);
    }
}