using restAPI.Models;

namespace TelephoneNetworkApi.Services
{
    public interface ISubscriberService
    {
        Task<IEnumerable<Subscriber>> ListAsync();
    }
}
