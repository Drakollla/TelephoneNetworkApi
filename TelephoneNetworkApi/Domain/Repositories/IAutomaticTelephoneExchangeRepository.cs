using TelephoneNetworkApi.Domain.Models;

namespace TelephoneNetworkApi.Domain.Repositories
{
    public interface IAutomaticTelephoneExchangeRepository
    {
        Task<IEnumerable<AutomaticTelephoneExchange>> ListAsync();
        Task AddAsync(AutomaticTelephoneExchange automaticTelephoneExchange);
        Task<AutomaticTelephoneExchange> FindByIdAsync(int id);
        void Update(AutomaticTelephoneExchange automaticTelephoneExchange);
        void Remove(AutomaticTelephoneExchange automaticTelephoneExchange);
    }
}