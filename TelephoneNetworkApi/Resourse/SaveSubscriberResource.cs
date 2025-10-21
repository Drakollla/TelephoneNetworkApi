using System.ComponentModel.DataAnnotations;

namespace TelephoneNetworkApi.Resourse
{
    public class SaveSubscriberResource
    {
        [Required]
        [MaxLength(30)]
        public string SecondName { get; set; }

        [Required]
        [MaxLength(30)]
        public string Name { get; set; }

        [Required]
        [MaxLength(30)]
        public string Surname { get; set; }

        [Required]
        [MaxLength(15)]
        public string PhoneNumber { get; set; }

        [Required]
        public bool IsIntercityOpen { get; set; }

        public ICollection<int> AutomaticTelephoneExchangeIds { get; set; } = new List<int>();
    }
}