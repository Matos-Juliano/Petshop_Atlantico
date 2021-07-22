using System.ComponentModel.DataAnnotations;

namespace Petshop_Atlantico.Models
{
    public class Owner
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        [MaxLength(100)]
        public string Address { get; set; }

        [Required]
        [MaxLength(5)]
        public string Number { get; set; }

        [Required]
        [MaxLength(100)]
        public string City { get; set; }

        [Required]
        public string PhoneNumber { get; set; }
    }
}