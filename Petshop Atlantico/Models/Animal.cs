using Petshop_Atlantico.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Petshop_Atlantico.Models
{
    public class Animal
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        public Owner Owner { get; set; }

        [Required]
        [MaxLength(250)]
        public string HospitalizationReason { get; set; }

        [Required]
        public HealthStatus HealthStatus { get; set; }

        public string Picture { get; set; }
    }
}
