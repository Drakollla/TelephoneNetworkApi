namespace TelephoneNetworkApi.Domain.Models
{
    /// <summary>
    /// АТС
    /// </summary>
    public class AutomaticTelephoneExchange
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Town { get; set; }
        public int CountSubscriber { get; set; }
        public ICollection<AtsSubscriber> AtsSubscribers { get; set; } = new List<AtsSubscriber>();
    }
}