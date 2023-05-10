using TelephoneNetworkApi.Models;
using TelephoneNetworkApi.Services.Communication;

namespace TelephoneNetworkApi.Services
{
    public interface ISubscriberService
    {
        Task<IEnumerable<Subscriber>> ListAsync();
        Task<SubscriberResponse> SaveAsync(Subscriber subscriber);
        Task<SubscriberResponse> UpdateAsync(int id, Subscriber subscriber);
        Task<SubscriberResponse> DeleteAsync(int id);
    }
}
