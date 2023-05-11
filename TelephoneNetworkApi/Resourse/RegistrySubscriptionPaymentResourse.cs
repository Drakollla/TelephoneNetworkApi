namespace TelephoneNetworkApi.Resourse
{
    public class RegistrySubscriptionPaymentResourse
    {
        public int Id { get; set; }
        public int Mounth { get; set; }
        public int Year { get; set; }
        public byte TownshipMinuteCount { get; set; }
        public byte IntecityMinuteCount { get; set; }
        public decimal Price { get; set; }
        public SubscriberResource Subscriber { get; set; }
    }
}
