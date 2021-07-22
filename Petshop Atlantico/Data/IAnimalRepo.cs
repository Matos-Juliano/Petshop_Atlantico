using Petshop_Atlantico.Enums;
using Petshop_Atlantico.Models;
using Petshop_Atlantico.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Petshop_Atlantico.Data
{
    public interface IAnimalRepo
    {
        public List<AnimalListModel> GetList(string name, string ownerName, HealthStatus? healthStatus, int pageSize, int pageIndex);

        public AnimalViewModel GetAnimalById(int id);

        public void Save(AnimalDTO animal);

        public void Update(AnimalDTO animal);
    }
}
