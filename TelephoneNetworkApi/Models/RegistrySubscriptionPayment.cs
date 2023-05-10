namespace TelephoneNetworkApi.Models
{
    /// <summary>
    /// Ведомость абонентской платы
    /// </summary>
    public class RegistrySubscriptionPayment
    {
        public int Id { get; set; }
        public DateTime Mounth { get; set; }
        public DateTime Year { get; set; }
        public byte TownshipMinuteCount { get; set; }
        public byte IntecityMinuteCount { get; set; }
        public decimal Price { get; set; }
        public decimal BenefitPrice { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
