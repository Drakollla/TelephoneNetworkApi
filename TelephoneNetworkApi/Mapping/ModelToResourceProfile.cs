using AutoMapper;
using restAPI.Models;
using TelephoneNetworkApi.Resourse;

namespace TelephoneNetworkApi.Mapping
{
    public class ModelToResourceProfile : Profile
    {
        public ModelToResourceProfile()
        {
            CreateMap<Subscriber, SubscriberResource>();
        }
    }
}
