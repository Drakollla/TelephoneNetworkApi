using AutoMapper;
using TelephoneNetworkApi.Models;
using TelephoneNetworkApi.Resourse;

namespace TelephoneNetworkApi.Mapping
{
    public class ResourceToModelProfile : Profile
    {
        public ResourceToModelProfile()
        {
            CreateMap<SaveSubscriberResource, Subscriber>();
        }
    }
}
