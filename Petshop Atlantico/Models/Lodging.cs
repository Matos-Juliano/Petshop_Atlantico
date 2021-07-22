using Petshop_Atlantico.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Petshop_Atlantico.Models
{
    public class Lodging
    {
        public int Id { get; set; }

        [Required]
        public OccupationStatus OccupationStatus { get; set; }

        public Animal Occupant { get; set; }
    }
}
