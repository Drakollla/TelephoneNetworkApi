using restAPI.Models;
using TelephoneNetworkApi.Repositories;
using TelephoneNetworkApi.Repozitories;
using TelephoneNetworkApi.Services;
using TelephoneNetworkApi.Services.Communication;

namespace TelephoneNetworkApi
{
    public class SubScriberService : ISubscriberService
    {
        private readonly ISubscriberRepositiry _subscriberRepositiry;
        private readonly IUnitOfWork _unitOfWork;

        public SubScriberService(ISubscriberRepositiry subscriberRepositiry, IUnitOfWork unitOfWork)
        {
            _subscriberRepositiry = subscriberRepositiry;
            _unitOfWork = unitOfWork;
        }

        public Task<IEnumerable<Subscriber>> ListAsync()
        {
            return _subscriberRepositiry.ListAsync();
        }

        public async Task<SaveSubscriberResponse> SaveAsync(Subscriber subscriber)
        {
            try
            {
                await _subscriberRepositiry.AddAsync(subscriber);
                await _unitOfWork.CompleteAsync();

                return new SaveSubscriberResponse(subscriber);
            }
            catch (Exception ex)
            {
                return new SaveSubscriberResponse($"An error occurred when saving the subscriber: {ex.Message}");
            }
        }
    }
}