using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Petshop_Atlantico.Enums
{
    public enum HealthStatus
    {
        [Display(Name ="Em tratamento")]
        InTreatment = 0,
        [Display(Name = "Recuperando")]
        Recovering = 1,
        [Display(Name = "Recuperado")]
        Recovered = 2
    }

    public enum OccupationStatus
    {
        [Display(Name = "Livre")]
        Free = 0,
        [Display(Name = "Ocupado")]
        Occupied = 1,
        [Display(Name = "Esperando Dono")]
        WaitingForOwner = 2
    }
}
