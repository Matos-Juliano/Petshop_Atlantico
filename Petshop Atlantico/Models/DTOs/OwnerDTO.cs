using System.ComponentModel.DataAnnotations;

namespace Petshop_Atlantico.Models
{
    public class OwnerDTO
    {
        public int? Id { get; set; }

        public string Name { get; set; }

        public string Address { get; set; }

        public string Number { get; set; }

        public string City { get; set; }

        public string PhoneNumber { get; set; }
    }
}