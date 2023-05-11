using AutoMapper;
using TelephoneNetworkApi.Domain.Models;
using TelephoneNetworkApi.Resourse;

namespace TelephoneNetworkApi.Mapping
{
    public class ResourceToModelProfile : Profile
    {
        public ResourceToModelProfile()
        {
            CreateMap<SaveSubscriberResource, Subscriber>();
            CreateMap<SaveAutomaticTelephoneExchangeResourse, AutomaticTelephoneExchange>();
            CreateMap<SaveRegistrySubscriptionPaymentResourse, RegistrySubscriptionPayment>();
        }
    }
}