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
    public class LodgingTest
    {
        [TestMethod]
        public void LodgingsController_GetUnnocupiedLodgingList_Success()
        {
            PetshopContext ctx = TestUtils.GetPetshopContext(MethodBase.GetCurrentMethod().Name.ToString());

            ILodgingRepo lodgingRepo = new LodgingRepository(ctx);

            for (int i = 0; i < 5; i++)
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

            for (int i = 0; i < 5; i++)
            {
                ctx.Lodgings.Add(new Lodging
                {
                    OccupationStatus = OccupationStatus.Free,
                    Occupant = null
                });

                ctx.SaveChanges();
            }

            IActionResult result;

            result = new LodgingsController(lodgingRepo).GetUnnocupiedLodgingList();

            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
            var okObjectResult = (OkObjectResult)result;
            List<int> lodgeList = (List<int>)okObjectResult.Value;

            Assert.IsTrue(lodgeList.Count == 5);
        }

        [TestMethod]
        public void LodgingsController_Index_Success()
        {
            PetshopContext ctx = TestUtils.GetPetshopContext(MethodBase.GetCurrentMethod().Name.ToString());

            ILodgingRepo lodgingRepo = new LodgingRepository(ctx);

            for (int i = 0; i < 5; i++)
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

            for (int i = 0; i < 5; i++)
            {
                ctx.Lodgings.Add(new Lodging
                {
                    OccupationStatus = OccupationStatus.Free,
                    Occupant = null
                });

                ctx.SaveChanges();
            }

            IActionResult result;

            result = new LodgingsController(lodgingRepo).Index();

            Assert.IsInstanceOfType(result, typeof(ViewResult));
            var okObjectResult = (ViewResult)result;
            List<Lodging> lodgeList = (List<Lodging>)okObjectResult.Model;

            Assert.IsTrue(lodgeList.Count == 10);
        }
    }
}
