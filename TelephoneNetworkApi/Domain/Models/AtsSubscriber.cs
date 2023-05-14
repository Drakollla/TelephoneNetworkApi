namespace TelephoneNetworkApi.Domain.Models
{
    public class AtsSubscriber
    {
        public int Id { get; set; }
        public int? SubscriberId { get; set; }
        public int? AutomaticTelephoneExchangeId { get; set; }
        public AutomaticTelephoneExchange? AutomaticTelephoneExchange { get; set; }
        public Subscriber? Subscriber { get; set; }
    }
}