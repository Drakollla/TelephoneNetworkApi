using TelephoneNetworkApi.Models;

namespace TelephoneNetworkApi.Repozitories
{
    public interface ISubscriberRepositiry
    {
        Task<IEnumerable<Subscriber>> ListAsync();
        Task AddAsync(Subscriber subscriber);
        Task<Subscriber> FindByIdAsync(int id);
        void Update(Subscriber subscriber);
        void Remove(Subscriber subscriber);
    }
}
