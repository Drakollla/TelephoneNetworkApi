using restAPI.Models;
using TelephoneNetworkApi.Services.Communication;

namespace TelephoneNetworkApi.Services
{
    public interface ISubscriberService
    {
        Task<IEnumerable<Subscriber>> ListAsync();
        Task<SaveSubscriberResponse> SaveAsync(Subscriber subscriber);
    }
}
