using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using RPPP21APP.Interfaces;
using RPPP21APP.Models;
using RPPP21APP.Repositories;
using RPPP21APP.ViewModels;

namespace RPPP21APP.Controllers
{
    public class PlantTypeController : Controller
    {

        private readonly IPlantTypeRepository _plantTypeRepository;
        private readonly IPlantBiologyRepository _plantBiologyRepository;

        public PlantTypeController(IPlantTypeRepository plantTypeRepository, IPlantBiologyRepository plantBiologyRepository)
        {
            _plantTypeRepository = plantTypeRepository;
            _plantBiologyRepository = plantBiologyRepository;
        }
        // GET: PlantTypeController
        public async Task<IActionResult> Index()
        {
            var planttypes = await _plantTypeRepository.GetAll();
            return View(planttypes);
        }

        // GET: PlantTypeController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: PlantTypeController/Create
        public async Task<ActionResult> CreateAsync()
        {
            ViewBag.PlantBiologyId = new SelectList(await _plantBiologyRepository.GetAll(), "PlantBiologyId", "Name");
            return View();
        }

        // POST: PlantTypeController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult<PlantType>> Create(CreatePlantTypeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.PlantBiologyId = new SelectList(await _plantBiologyRepository.GetAll(), "PlantBiologyId", "Name");
                return View("Create");
            }

            var type = new PlantType
            {
                Type = model.Type,
                CaloriesPer100g= model.CaloriesPer100g,
                FatPer100g= model.FatPer100g,
                ProteinPer100g= model.ProteinPer100g,
                FiberPer100g= model.FiberPer100g,
                CarbsPer100g= model.CarbsPer100g,
                Vitamins = model.Vitamins,
                PlantBiologyId = model.PlantBiologyId

            };
            try
            {
                _plantTypeRepository.Add(type);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: PlantTypeController/Edit/5
        public async Task<ActionResult> EditAsync(int id)
        {
            var planttype = await _plantTypeRepository.GetByIdAsync(id);
            if (planttype == null)
            {
                return NotFound();
            }

            ViewBag.PlantBiologyId = new SelectList(await _plantBiologyRepository.GetAll(), "PlantBiologyId", "Name");

            return View(planttype);
        }

        // POST: PlantTypeController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditAsync(int id, CreatePlantTypeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.PlantBiologyId = new SelectList(await _plantBiologyRepository.GetAll(), "PlantBiologyId", "Name");

                return View("Edit");
            }

            var planttype = await _plantTypeRepository.GetByIdAsync(id);
            if (planttype == null)
            {
                return NotFound();
            }

            planttype.Type = model.Type;
            planttype.CaloriesPer100g = model.CaloriesPer100g;
            planttype.FatPer100g = model.FatPer100g;
            planttype.ProteinPer100g = model.ProteinPer100g;
            planttype.FiberPer100g = model.FiberPer100g;
            planttype.CarbsPer100g = model.CarbsPer100g;
            planttype.Vitamins = model.Vitamins;
            planttype.PlantBiologyId = model.PlantBiologyId;
            try
            {
                _plantTypeRepository.Update(planttype);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: PlantTypeController/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            var passport = await _plantTypeRepository.GetByIdAsync(id);
            if (passport == null)
            {
                // If the infrastructure object is not found, return a 404 error
                return NotFound();
            }

            // Pass the infrastructure object to the view
            return View(passport);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            // Get the infrastructure object with the given id
            var passport = await _plantTypeRepository.GetByIdAsync(id);
            if (passport == null)
            {
                // If the infrastructure object is not found, return a 404 error
                return NotFound();
            }

            // Delete the infrastructure object from the database
            _plantTypeRepository.Delete(passport);
            // Redirect to the index page

            // Otherwise, redirect to the Index action in the current controller
            return RedirectToAction("Index");

        }
    }
}
