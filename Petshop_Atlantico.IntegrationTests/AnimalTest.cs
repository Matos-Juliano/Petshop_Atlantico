using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.EntityFrameworkCore.InMemory;
using Petshop_Atlantico.Controllers;
using Petshop_Atlantico.Data;
using System.Reflection;
using Petshop_Atlantico.Models;
using Petshop_Atlantico.Enums;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace Petshop_Atlantico.IntegrationTests
{
    [TestClass]
    public class AnimalTest
    {

        [TestMethod]
        public void AnimalController_AnimalDetails_Success()
        {
            PetshopContext ctx = TestUtils.GetPetshopContext(MethodBase.GetCurrentMethod().Name.ToString());

            IAnimalRepo animalRepo = new AnimalRepository(ctx, null, null);
            Animal anim = new Animal
            {
                Name = "Dorado",
                HospitalizationReason = "Vomitando.",
                HealthStatus = HealthStatus.InTreatment,
                Picture = null,
                Owner = new Owner
                {
                    Name = "Juliano Matos",
                    PhoneNumber = "54996430783",
                    City = "Fortaleza",
                    Address = "Rua Marcos Macedo",
                    Number = "200"
                }
            };

            ctx.Animals.Add(anim);
            ctx.SaveChanges();

            ctx.Lodgings.Add(new Lodging
            {
                OccupationStatus = OccupationStatus.Occupied,
                Occupant = anim
            });
            ctx.SaveChanges();

            IActionResult result;

            result = new AnimalsController(animalRepo).AnimalDetails(1);

            Assert.IsInstanceOfType(result, typeof(ViewResult));
            var okResult = (ViewResult)result;
            AnimalViewModel animal = (AnimalViewModel)okResult.Model;

            Assert.IsInstanceOfType(animal, typeof(AnimalViewModel));

            Assert.IsNotNull(animal.Name);
            Assert.IsNotNull(animal.HospitalizationReason);
            Assert.IsNotNull(animal.HealthStatus);
            Assert.IsNotNull(animal.Owner);
            Assert.IsNotNull(animal.Lodge);
        }

        [TestMethod]
        public void AnimalController_AnimalDetails_Error()
        {
            PetshopContext ctx = TestUtils.GetPetshopContext(MethodBase.GetCurrentMethod().Name.ToString());

            IAnimalRepo animalRepo = new AnimalRepository(ctx, null, null);

            ctx.Animals.Add(new Animal
            {
                Name = "Dorado",
                HospitalizationReason = "Vomitando.",
                HealthStatus = HealthStatus.InTreatment,
                Picture = null,
                Owner = new Owner
                {
                    Name = "Juliano Matos",
                    PhoneNumber = "54996430783",
                    City = "Fortaleza",
                    Address = "Rua Marcos Macedo",
                    Number = "200"
                }
            });
            ctx.SaveChanges();

            Assert.ThrowsException<NullReferenceException>(() => new AnimalsController(animalRepo).AnimalDetails(10));
        }

        [TestMethod]
        public void AnimalController_GetAnimalListFromDatabase_Success()
        {
            PetshopContext ctx = TestUtils.GetPetshopContext(MethodBase.GetCurrentMethod().Name.ToString());

            IAnimalRepo animalRepo = new AnimalRepository(ctx, null, null);

            for(int i = 0; i < 5; i++)
            {
                Animal anim = new Animal
                {
                    Name = "Dorado",
                    HospitalizationReason = "Vomitando.",
                    HealthStatus = HealthStatus.InTreatment,
                    Picture = null,
                    Owner = new Owner
                    {
                        Name = "Juliano Matos",
                        PhoneNumber = "54996430783",
                        City = "Fortaleza",
                        Address = "Rua Marcos Macedo",
                        Number = "200"
                    }
                };

                ctx.Animals.Add(anim);
                ctx.SaveChanges();

                ctx.Lodgings.Add(new Lodging
                {
                    OccupationStatus = OccupationStatus.Occupied,
                    Occupant = anim
                });
                ctx.SaveChanges();
            }

            IActionResult result;

            result = new AnimalsController(animalRepo).GetAnimalList(new AnimalListModel());

            Assert.IsInstanceOfType(result, typeof(PartialViewResult));
            var partialViewResult = (PartialViewResult)result;
            List<AnimalListModel> animal = (List<AnimalListModel>)partialViewResult.Model;

            Assert.IsTrue(animal.Count == 5);
        }
    }
}
