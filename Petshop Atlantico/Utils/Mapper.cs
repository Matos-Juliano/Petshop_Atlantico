using Petshop_Atlantico.Enums;
using Petshop_Atlantico.Models;
using Petshop_Atlantico.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Petshop_Atlantico.Utils
{
    public static class Mapper
    {
        public static AnimalDTO MapAnimalListModelToAnimalDTO(AnimalViewModel model)
        {
            AnimalDTO animalDTO = new AnimalDTO()
            {
                Id = model.Id != null ? model.Id : null,
                Name = model.Name,
                HealthStatus = model.HealthStatus != null ? model.HealthStatus : null,
                Owner = new OwnerDTO
                {
                    Id = model.Owner.Id,
                    Name = model.Owner.Name,
                    PhoneNumber = model.Owner.PhoneNumber != null ? Utils.FilterPhoneNumber(model.Owner.PhoneNumber) : null,
                    Address = model.Owner.Address,
                    Number = model.Owner.Number,
                    City = model.Owner.City
                },
                HospitalizationReason = model.HospitalizationReason,
                Lodge = model.Lodge.Id,
                PictureFile = model.PictureFile != null ? model.PictureFile : null
            };

            return animalDTO;
        }
    }
}
