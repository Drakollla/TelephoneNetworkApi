using TelephoneNetworkApi.Domain.Models;

namespace TelephoneNetworkApi.Domain.Repositories
{
    public interface ISubscriberRepository
    {
        Task<IEnumerable<Subscriber>> ListAsync();
        Task AddAsync(Subscriber subscriber);
        Task<Subscriber> FindByIdAsync(int id);
        void Update(Subscriber subscriber);
        void Remove(Subscriber subscriber);
    }
}