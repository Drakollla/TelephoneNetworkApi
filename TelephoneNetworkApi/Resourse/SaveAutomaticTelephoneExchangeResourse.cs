using System.ComponentModel.DataAnnotations;

namespace TelephoneNetworkApi.Resourse
{
    public class SaveAutomaticTelephoneExchangeResourse
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        [MaxLength(30)]
        public string Town { get; set; }

        [Required]
        public int CountSubscriber { get; set; }
    }
}