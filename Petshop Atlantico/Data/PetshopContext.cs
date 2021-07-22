using Microsoft.EntityFrameworkCore;
using Petshop_Atlantico.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Petshop_Atlantico.Data
{
    public class PetshopContext : DbContext
    {

        public PetshopContext(DbContextOptions<PetshopContext> opt) : base (opt)
        {

        }

        public DbSet<Animal> Animals { get; set; }

        public DbSet<Owner> Owners { get; set; }

        public DbSet<Lodging> Lodgings { get; set; }
    }
}
