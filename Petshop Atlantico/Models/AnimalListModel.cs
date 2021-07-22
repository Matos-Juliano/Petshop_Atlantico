using Microsoft.AspNetCore.Http;
using Petshop_Atlantico.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Petshop_Atlantico.Models
{
    public class AnimalListModel
    {
        public int? Id { get; set; }

        public string Name { get; set; }

        public string OwnerName { get; set; }

        public HealthStatus? HealthStatus { get; set; }

        public int? Lodging { get; set; }

        public int PageSize { get; set; }

        public int PageIndex { get; set; }
    }
}
