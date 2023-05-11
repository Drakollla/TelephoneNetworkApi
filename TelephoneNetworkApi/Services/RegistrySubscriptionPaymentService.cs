using TelephoneNetworkApi.Domain.Models;
using TelephoneNetworkApi.Domain.Repositories;
using TelephoneNetworkApi.Domain.Services;
using TelephoneNetworkApi.Domain.Services.Communication;

namespace TelephoneNetworkApi.Services
{
    public class RegistrySubscriptionPaymentService : IRegistrySubscriptionPaymentService
    {
        private readonly IRegistrySubscriptionPaymentRepository _registrySubscriptionPaymentRepository;
        private readonly ISubscriberRepository _subscriberRepository;
        private readonly IUnitOfWork _unitOfWork;

        public RegistrySubscriptionPaymentService(IRegistrySubscriptionPaymentRepository registrySubscriptionPaymentRepository, ISubscriberRepository subscriberRepository, IUnitOfWork unitOfWork)
        {
            _registrySubscriptionPaymentRepository = registrySubscriptionPaymentRepository;
            _subscriberRepository = subscriberRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<RegistrySubscriptionPayment>> ListAsync()
        {
            return await _registrySubscriptionPaymentRepository.ListAsync();
        }

        public async Task<RegistrySubscriptionPaymentResponse> SaveAsync(RegistrySubscriptionPayment registrySubscriptionPayment)
        {
            try
            {
                var existingSubscriber = await _subscriberRepository.FindByIdAsync(registrySubscriptionPayment.SubscriberId);
                if (existingSubscriber == null)
                    return new RegistrySubscriptionPaymentResponse("Invalid subscriber.");

                await _registrySubscriptionPaymentRepository.AddAsync(registrySubscriptionPayment);
                await _unitOfWork.CompleteAsync();

                return new RegistrySubscriptionPaymentResponse(registrySubscriptionPayment);
            }
            catch (Exception ex)
            {
                // Do some logging stuff
                return new RegistrySubscriptionPaymentResponse($"An error occurred when saving the payment: {ex.Message}");
            }
        }

        public async Task<RegistrySubscriptionPaymentResponse> UpdateAsync(int id, RegistrySubscriptionPayment registrySubscriptionPayment)
        {
            var existingPayment = await _registrySubscriptionPaymentRepository.FindByIdAsync(id);

            if (existingPayment == null)
                return new RegistrySubscriptionPaymentResponse("Payment not found.");

            var existingSubscriber = await _subscriberRepository.FindByIdAsync(registrySubscriptionPayment.SubscriberId);
            if (existingSubscriber == null)
                return new RegistrySubscriptionPaymentResponse("Invalid category.");

            existingPayment.Price = registrySubscriptionPayment.Price;

            try
            {
                _registrySubscriptionPaymentRepository.Update(existingPayment);
                await _unitOfWork.CompleteAsync();

                return new RegistrySubscriptionPaymentResponse(existingPayment);
            }
            catch (Exception ex)
            {
                // Do some logging stuff
                return new RegistrySubscriptionPaymentResponse($"An error occurred when updating the payment: {ex.Message}");
            }
        }

        public async Task<RegistrySubscriptionPaymentResponse> DeleteAsync(int id)
        {
            var existingCategory = await _registrySubscriptionPaymentRepository.FindByIdAsync(id);

            if (existingCategory == null)
                return new RegistrySubscriptionPaymentResponse("Payment not found.");

            try
            {
                _registrySubscriptionPaymentRepository.Remove(existingCategory);
                await _unitOfWork.CompleteAsync();

                return new RegistrySubscriptionPaymentResponse(existingCategory);
            }
            catch (Exception ex)
            {
                // Do some logging stuff
                return new RegistrySubscriptionPaymentResponse($"An error occurred when deleting the payment: {ex.Message}");
            }
        }
    }
}
