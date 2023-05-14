using AutoMapper;
using TelephoneNetworkApi.Domain.Models;
using TelephoneNetworkApi.Resourse;

namespace TelephoneNetworkApi.Mapping
{
    public class ModelToResourceProfile : Profile
    {
        public ModelToResourceProfile()
        {
            CreateMap<Subscriber, SubscriberResourse>();
            CreateMap<AutomaticTelephoneExchange, AutomaticTelephoneExchangeResourse>();
            CreateMap<RegistrySubscriptionPayment, RegistrySubscriptionPaymentResourse>();
        }
    }
}