using TelephoneNetworkApi.Domain.Models;
using TelephoneNetworkApi.Domain.Repositories;
using TelephoneNetworkApi.Domain.Services;
using TelephoneNetworkApi.Domain.Services.Communication;

namespace TelephoneNetworkApi.Services
{
    public class AutomaticTelephoneExchangeService : IAutomaticTelephoneExchangeService
    {
        private readonly IAutomaticTelephoneExchangeRepository _automaticTelephoneExchangeRepository;
        private readonly IUnitOfWork _unitOfWork;

        public AutomaticTelephoneExchangeService(IAutomaticTelephoneExchangeRepository automaticTelephoneExchangeRepository, IUnitOfWork unitOfWork)
        {
            _automaticTelephoneExchangeRepository = automaticTelephoneExchangeRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<AutomaticTelephoneExchange>> ListAsync()
        {
            return await _automaticTelephoneExchangeRepository.ListAsync();
        }

        public async Task<AutomaticTelephoneExchangeResponse> SaveAsync(AutomaticTelephoneExchange automaticTelephoneExchange)
        {
            try
            {
                await _automaticTelephoneExchangeRepository.AddAsync(automaticTelephoneExchange);
                await _unitOfWork.CompleteAsync();

                return new AutomaticTelephoneExchangeResponse(automaticTelephoneExchange);
            }
            catch (Exception ex)
            {
                return new AutomaticTelephoneExchangeResponse($"An error occurred when saving the ATS: {ex.Message}");
            }
        }

        public async Task<AutomaticTelephoneExchangeResponse> UpdateAsync(int id, AutomaticTelephoneExchange automaticTelephoneExchange)
        {
            var existingATS = await _automaticTelephoneExchangeRepository.FindByIdAsync(id);

            if (existingATS == null)
                return new AutomaticTelephoneExchangeResponse("ATS not found.");

            existingATS.Name = automaticTelephoneExchange.Name;
            existingATS.Town = automaticTelephoneExchange.Town;
            existingATS.CountSubscriber = automaticTelephoneExchange.CountSubscriber;

            try
            {
                _automaticTelephoneExchangeRepository.Update(existingATS);
                await _unitOfWork.CompleteAsync();

                return new AutomaticTelephoneExchangeResponse(existingATS);
            }
            catch (Exception ex)
            {
                return new AutomaticTelephoneExchangeResponse($"An error occurred when updating the ATS: {ex.Message}");
            }
        }

        public async Task<AutomaticTelephoneExchangeResponse> DeleteAsync(int id)
        {
            var existingATS = await _automaticTelephoneExchangeRepository.FindByIdAsync(id);

            if (existingATS == null)
                return new AutomaticTelephoneExchangeResponse("Ats not found.");

            try
            {
                _automaticTelephoneExchangeRepository.Remove(existingATS);
                await _unitOfWork.CompleteAsync();

                return new AutomaticTelephoneExchangeResponse(existingATS);
            }
            catch (Exception ex)
            {
                return new AutomaticTelephoneExchangeResponse($"An error occurred when deleting the ats: {ex.Message}");
            }
        }
    }
}