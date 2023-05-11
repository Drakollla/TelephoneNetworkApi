namespace TelephoneNetworkApi.Domain.Models
{
    /// <summary>
    /// Ведомость абонентской платы
    /// </summary>
    public class RegistrySubscriptionPayment
    {
        public int Id { get; set; }
        public int Mounth { get; set; }
        public int Year { get; set; }
        public byte TownshipMinuteCount { get; set; }
        public byte IntecityMinuteCount { get; set; }
        public decimal Price { get; set; }

        public int SubscriberId { get; set; }
        public Subscriber Subscriber { get; set; }
    }
}