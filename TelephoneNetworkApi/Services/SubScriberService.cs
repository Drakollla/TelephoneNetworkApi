using TelephoneNetworkApi.Domain.Models;
using TelephoneNetworkApi.Domain.Repositories;
using TelephoneNetworkApi.Services.Communication;

namespace TelephoneNetworkApi.Services
{
    public class SubScriberService : ISubscriberService
    {
        private readonly ISubscriberRepository _subscriberRepository;
        private readonly IUnitOfWork _unitOfWork;

        public SubScriberService(ISubscriberRepository subscriberRepositiry, IUnitOfWork unitOfWork)
        {
            _subscriberRepository = subscriberRepositiry;
            _unitOfWork = unitOfWork;
        }

        public Task<IEnumerable<Subscriber>> ListAsync()
        {
            return _subscriberRepository.ListAsync();
        }

        public async Task<SubscriberResponse> SaveAsync(Subscriber subscriber)
        {
            try
            {
                await _subscriberRepository.AddAsync(subscriber);
                await _unitOfWork.CompleteAsync();

                return new SubscriberResponse(subscriber);
            }
            catch (Exception ex)
            {
                return new SubscriberResponse($"An error occurred when saving the subscriber: {ex.Message}");
            }
        }

        public async Task<SubscriberResponse> UpdateAsync(int id, Subscriber subscriber)
        {
            var existingSubscriber = await _subscriberRepository.FindByIdAsync(id);

            if (existingSubscriber == null)
                return new SubscriberResponse("Subscriber not found.");

            existingSubscriber.SecondName = subscriber.SecondName;
            existingSubscriber.Name = subscriber.Name;
            existingSubscriber.Surname = subscriber.Surname;
            existingSubscriber.PhoneNumber = subscriber.PhoneNumber;
            existingSubscriber.IsIntercityOpen = subscriber.IsIntercityOpen;

            try
            {
                _subscriberRepository.Update(existingSubscriber);
                await _unitOfWork.CompleteAsync();

                return new SubscriberResponse(existingSubscriber);
            }
            catch (Exception ex)
            {
                return new SubscriberResponse($"An error occurred when updating the subscriber: {ex.Message}");
            }
        }

        public async Task<SubscriberResponse> DeleteAsync(int id)
        {
            var existingCategory = await _subscriberRepository.FindByIdAsync(id);

            if (existingCategory == null)
                return new SubscriberResponse("Category not found.");

            try
            {
                _subscriberRepository.Remove(existingCategory);
                await _unitOfWork.CompleteAsync();

                return new SubscriberResponse(existingCategory);
            }
            catch (Exception ex)
            {
                return new SubscriberResponse($"An error occurred when deleting the subscriber: {ex.Message}");
            }
        }
    }
}