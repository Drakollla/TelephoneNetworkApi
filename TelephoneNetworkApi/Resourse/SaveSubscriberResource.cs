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

        public string PhoneNumber { get; set; }
        public bool IsIntercityOpen { get; set; }
    }
}