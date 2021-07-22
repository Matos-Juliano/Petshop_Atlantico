using Microsoft.EntityFrameworkCore;
using Petshop_Atlantico.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Petshop_Atlantico.IntegrationTests
{
    public static class TestUtils
    {
        public static PetshopContext GetPetshopContext(string name)
        {
            DbContextOptionsBuilder<PetshopContext> optionsBuilder = new DbContextOptionsBuilder<PetshopContext>();
            optionsBuilder.UseInMemoryDatabase(name);

            return new PetshopContext(optionsBuilder.Options);
        }
    }
}
