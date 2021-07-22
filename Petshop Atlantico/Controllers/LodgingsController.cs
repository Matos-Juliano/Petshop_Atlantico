using Microsoft.AspNetCore.Mvc;
using Petshop_Atlantico.Data;
using Petshop_Atlantico.Enums;
using Petshop_Atlantico.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Petshop_Atlantico.Controllers
{
    public class LodgingsController : Controller
    {
        private readonly ILodgingRepo _lodgingRepo;

        public LodgingsController(ILodgingRepo lodgingRepo)
        {
            _lodgingRepo = lodgingRepo;
        }

        public IActionResult Index()
        {
            List<Lodging> lodgings = _lodgingRepo.GetList();

            return View("~/Views/Lodgings/LodgingList.cshtml", lodgings);
        }

        [HttpGet]
        public IActionResult GetUnnocupiedLodgingList()
        {
            try
            {
                List<int> list = _lodgingRepo.GetUnnocupiedList();

                return Ok(list);
            }
            catch(Exception ex)
            {
                return Json(ex.Message);
            }
        }

        [HttpPost]
        public IActionResult DeliverAnimal(int id)
        {
            _lodgingRepo.UpdateOccupationStatus(id, OccupationStatus.Free);

            return RedirectToAction(nameof(Index));
        }
    }
}
