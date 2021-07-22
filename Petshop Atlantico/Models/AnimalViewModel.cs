using Microsoft.AspNetCore.Http;
using Petshop_Atlantico.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Petshop_Atlantico.Models
{
    public class AnimalViewModel
    {
        public int? Id { get; set; }

        public string Name { get; set; }

        public Owner Owner { get; set; }

        public string HospitalizationReason { get; set; }

        public HealthStatus? HealthStatus { get; set; }

        public string Picture { get; set; }

        public IFormFile PictureFile { get; set; }

        public Lodging Lodge { get; set; }
    }
}
