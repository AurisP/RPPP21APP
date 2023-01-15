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
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: PlantTypeController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: PlantTypeController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: PlantTypeController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
