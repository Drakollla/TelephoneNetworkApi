namespace TelephoneNetworkApi.Models
{
    /// <summary>
    /// Прайс АТС
    /// </summary>
    public class PriceAutomaticTelephoneExchange
    {
        public decimal TownshipPrice { get; set; }
        public decimal IntercityPrice { get; set; } 
        public int AutomaticTelephoneExchangeId { get; set; }
    }
}
