using System.ComponentModel.DataAnnotations;
using TelephoneNetworkApi.Domain.Models;

namespace TelephoneNetworkApi.Resourse
{
    public class SaveRegistrySubscriptionPaymentResourse
    {
        [Required]
        public int Mounth { get; set; }

        [Required]
        public int Year { get; set; }

        [Required]
        public byte TownshipMinuteCount { get; set; }

        [Required]
        public byte IntecityMinuteCount { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        public int SubscriberId { get; set; }
    }
}
