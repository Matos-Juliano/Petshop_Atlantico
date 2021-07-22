using Microsoft.AspNetCore.Http;
using Petshop_Atlantico.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Petshop_Atlantico.Models.DTOs
{
    public class AnimalDTO
    {
        public int? Id { get; set; }

        public string Name { get; set; }

        public OwnerDTO Owner { get; set; }

        public string HospitalizationReason { get; set; }

        public HealthStatus? HealthStatus { get; set; }

        public string Picture { get; set; }

        public int Lodge { get; set; }

        public IFormFile PictureFile { get; set; }
    }
}
