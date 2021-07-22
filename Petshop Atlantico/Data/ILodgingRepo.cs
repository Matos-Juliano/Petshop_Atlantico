using Petshop_Atlantico.Enums;
using Petshop_Atlantico.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Petshop_Atlantico.Data
{
    public interface ILodgingRepo
    {
        public List<Lodging> GetList();

        public List<int> GetUnnocupiedList();

        public Lodging GetLodgingById(int id);

        public Lodging GetLodgingByOccupant(int id);

        public void UpdateOccupationStatus(int id, OccupationStatus occupationStatus);
    }
}
