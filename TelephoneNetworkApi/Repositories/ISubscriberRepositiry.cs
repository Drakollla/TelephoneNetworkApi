using restAPI.Models;

namespace TelephoneNetworkApi.Repozitories
{
    public interface ISubscriberRepositiry
    {
        Task<IEnumerable<Subscriber>> ListAsync();
        Task AddAsync(Subscriber subscriber);
    }
}
