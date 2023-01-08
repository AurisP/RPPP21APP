using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using RPPP21APP.Interfaces;
using RPPP21APP.Models;
using RPPP21APP.Repositories;
using RPPP21APP.Repository;
using RPPP21APP.ViewModels;

namespace RPPP21APP.Controllers
{
    public class PlantController : Controller
    {
        private readonly IPlantRepository _plantRepository;
        private readonly IGroupOfPlants _groupOfPlants;

        public PlantController(IPlantRepository plantRepository, IGroupOfPlants groupOfPlants)
        {
            _plantRepository = plantRepository;
            _groupOfPlants = groupOfPlants;
        }
        // GET: PlantController
        public async Task<IActionResult> Index()
        {
            var plants = await _plantRepository.GetAll();
            return View(plants);
        }

        // GET: PlantController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: PlantController/Create
        public async Task<ActionResult> Create()
        {
            ViewBag.GroupOfPlantsId = new SelectList(await _groupOfPlants.GetAll(), "GroupOfPlantsId", "GroupOfPlantsId");
            return View();
        }

        // POST: PlantController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult<Plant>> Create(CreatePlantViewModel model)
        {
            var plant = new Plant
            {
                Name = model.Name,
                GroupOfPlantsId = model.GroupOfPlantsId
            };
            try
            {
                _plantRepository.Add(plant);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: PlantController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: PlantController/Edit/5
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

        // GET: PlantController/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            // Get the infrastructure object with the given id
            var plant = await _plantRepository.GetByIdAsync(id);
            if (plant == null)
            {
                // If the infrastructure object is not found, return a 404 error
                return NotFound();
            }

            // Pass the infrastructure object to the view
            return View(plant);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            // Get the infrastructure object with the given id
            var plant = await _plantRepository.GetByIdAsync(id);
            if (plant == null)
            {
                // If the infrastructure object is not found, return a 404 error
                return NotFound();
            }

            // Delete the infrastructure object from the database
            _plantRepository.Delete(plant);
            // Redirect to the index page

            // Otherwise, redirect to the Index action in the current controller
            return RedirectToAction("Index");

        }
    }
}
