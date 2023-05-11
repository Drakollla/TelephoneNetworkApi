using TelephoneNetworkApi.Domain.Models;
using TelephoneNetworkApi.Domain.Services.Communication;

namespace TelephoneNetworkApi.Domain.Services
{
    public interface IAutomaticTelephoneExchangeService
    {
        Task<IEnumerable<AutomaticTelephoneExchange>> ListAsync();
        Task<AutomaticTelephoneExchangeResponse> SaveAsync(AutomaticTelephoneExchange automaticTelephoneExchange);
        Task<AutomaticTelephoneExchangeResponse> UpdateAsync(int id, AutomaticTelephoneExchange automaticTelephoneExchange);
        Task<AutomaticTelephoneExchangeResponse> DeleteAsync(int id);
    }
}