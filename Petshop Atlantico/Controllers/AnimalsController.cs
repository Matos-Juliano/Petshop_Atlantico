using Microsoft.AspNetCore.Mvc;
using Petshop_Atlantico.Data;
using Petshop_Atlantico.Enums;
using Petshop_Atlantico.Models;
using Petshop_Atlantico.Models.DTOs;
using Petshop_Atlantico.Utils;
using System;
using System.Collections.Generic;

namespace Petshop_Atlantico.Controllers
{
    public class AnimalsController : Controller
    {
        private readonly IAnimalRepo _animalRepo;

        public AnimalsController(IAnimalRepo animalRepo)
        {
            _animalRepo = animalRepo;
        }
        [ActionName("Index")]
        public IActionResult Index()
        {
            return View("~/Views/Animals/AnimalIndex.cshtml", new AnimalListModel());
        }

        [HttpGet]
        public IActionResult AddAnimal()
        {
            AnimalViewModel animal = new AnimalViewModel();
            return View("~/Views/Animals/AddAnimal.cshtml", animal);
        }

        [HttpGet("animals/view/{id}")]
        public IActionResult AnimalDetails(int id)
        {
            AnimalViewModel animal = _animalRepo.GetAnimalById(id);
            return View("~/Views/Animals/AnimalDetails.cshtml", animal);
        }

        [HttpPost]
        public IActionResult AddNewAnimal(AnimalViewModel animal)
        {
            try
            {
                AnimalDTO animalDTO = Mapper.MapAnimalListModelToAnimalDTO(animal);
                _animalRepo.Save(animalDTO);
                return RedirectToAction(nameof(Index));
            }
            catch(Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost]
        public IActionResult GetAnimalList(AnimalListModel animal)
        {
            try
            {
                List<AnimalListModel> animals = _animalRepo.GetList(animal.Name, animal.OwnerName, animal.HealthStatus, animal.PageSize, animal.PageIndex);

                return PartialView("~/Views/Animals/AnimalList.cshtml", animals);
            }
            catch(Exception ex)
            {
                return BadRequest(ex);
            }
            
        }

        [HttpGet("/animals/{id}")]
        public IActionResult GoToEditAnimal(int id)
        {
            try
            {
                AnimalViewModel animal = _animalRepo.GetAnimalById(id);
                return View("~/Views/Animals/EditAnimal.cshtml", animal);
            }
            catch(Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost]
        public IActionResult EditAnimal(AnimalViewModel animal)
        {
            try
            {
                AnimalDTO animalDTO = Mapper.MapAnimalListModelToAnimalDTO(animal);
                _animalRepo.Update(animalDTO);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        public JsonResult GetHealthStatusComboBox()
        {
            List<ComboBoxModel> comboBoxModelList = new List<ComboBoxModel>();
            foreach (HealthStatus item in HealthStatus.GetValues(typeof(HealthStatus)))
            {
                comboBoxModelList.Add(new ComboBoxModel { Name = Utils.Utils.GetEnumDisplayAttribute(item), Value = (int)item });
            };

            return Json(comboBoxModelList);
        }
    }
}
