namespace TelephoneNetworkApi.Models
{
    /// <summary>
    /// Ведомость звонков
    /// </summary>
    public class RegistryCall
    {
        public int Id { get; set; }
        public DateTime DateCall { get; set; }
        public TimeOnly StartTime { get; set; }
        public TimeOnly EndTime { get; set; }
        public bool IsIntercity { get; set; }
        public int SubscriberId { get; set; }
    }
}
