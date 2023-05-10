namespace TelephoneNetworkApi.Models
{
    /// <summary>
    /// Абоненты
    /// </summary>
    public class Subscriber
    {
        public int Id { get; set; }
        public string SecondName { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string PhoneNumber { get; set; }
        public bool IsIntercityOpen { get; set; }
        public bool HasBenefit { get; set; }
    }
}
