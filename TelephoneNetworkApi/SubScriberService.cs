using restAPI.Models;
using TelephoneNetworkApi.Repozitories;
using TelephoneNetworkApi.Services;

namespace TelephoneNetworkApi
{
    public class SubScriberService : ISubscriberService
    {
        private readonly ISubscriberRepositiry _subscriberRepositiry;

        public SubScriberService(ISubscriberRepositiry subscriberRepositiry)
        {
            _subscriberRepositiry = subscriberRepositiry;
        }

        public Task<IEnumerable<Subscriber>> ListAsync()
        {
            return _subscriberRepositiry.ListAsync();
        }
    }
}