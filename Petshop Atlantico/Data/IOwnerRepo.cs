using Petshop_Atlantico.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Petshop_Atlantico.Data
{
    public interface IOwnerRepo
    {
        public IEnumerable<Owner> GetList();

        public Owner GetOwnerById(int id);

        public void Save(Owner animal);
    }
}
