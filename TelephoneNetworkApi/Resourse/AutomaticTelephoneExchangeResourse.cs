using TelephoneNetworkApi.Domain.Models;

namespace TelephoneNetworkApi.Resourse
{
    public class AutomaticTelephoneExchangeResourse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Town { get; set; }
        public int CountSubscriber { get; set; }
        public ICollection<AtsSubscriber> AtsAndSubscribers { get; set; }
    }
}