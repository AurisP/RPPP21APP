using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using RPPP21APP.Interfaces;
using RPPP21APP.Models;
using RPPP21APP.Repositories;
using RPPP21APP.Repository;
using RPPP21APP.ViewModels;
using System.Collections.Generic;

namespace RPPP21APP.Controllers
{
    public class PlantController : Controller
    {
        private readonly IPlantRepository _plantRepository;
        private readonly IGroupOfPlants _groupOfPlants;
        private readonly IPlantTypeRepository _plantTypeRepository;
        private readonly IPlotRepository _plotRepository;

        public PlantController(IPlantRepository plantRepository, IGroupOfPlants groupOfPlants, IPlantTypeRepository plantTypeRepository,
            IPlotRepository plotRepository)
        {
            _plantRepository = plantRepository;
            _groupOfPlants = groupOfPlants;
            _plantTypeRepository = plantTypeRepository;
            _plotRepository = plotRepository;
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
            ViewBag.PlotId = new SelectList(await _plotRepository.GetAll(), "PlotId", "Name");
            ViewBag.PlantTypeId = new SelectList(await _plantTypeRepository.GetAll(), "PlantTypeId", "Type");
            return View();
        }

        // POST: PlantController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult<Plant>> Create(CreatePlantViewModel model)
        {
            GroupOfPlant groupOfPlant = await _groupOfPlants.GetByIdAsyncNoTrack(model.PlotId, model.PlantTypeId);

            if (groupOfPlant == null)
            {
                return NotFound(); //Should be message of "This group on this plot doesn't exist. Create group first              
            }

            var plant = new Plant
            {
                Name = model.Name,
                GroupOfPlantsId = groupOfPlant.GroupOfPlantsId
            };
            try
            {
                Console.WriteLine("TEST TEST ");
                Console.WriteLine(plant.GroupOfPlantsId);
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
