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
    public class PassportController : Controller
    {
        private readonly IPassportRepository _passportRepository;
        private readonly IPlantRepository _plantRepository;
        public PassportController(IPassportRepository passportRepository, IPlantRepository plantRepository)
        {
            _passportRepository = passportRepository;
            _plantRepository = plantRepository;
        }
        // GET: PassportController
        public async Task<IActionResult> Index()
        {
            var passports = await _passportRepository.GetAll();
            return View(passports);
        }

        // GET: PassportController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: PassportController/Create
        public async Task<ActionResult> Create()
        {
            ViewBag.PlantId = new SelectList(await _plantRepository.GetAll(), "PlantId", "Name");
            return View();
        }

        // POST: PassportController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult<Passport>> Create(CreatePassportViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.PlantId = new SelectList(await _plantRepository.GetAll(), "PlantId", "Name");
                return View("Create");
            }

            var passport = new Passport
            {
                LatinName = model.LatinName,
                Origin = model.Origin,
                MotherFarm = model.MotherFarm,
                LinkToFloraCroatia= model.LinkToFloraCroatia,
                PlantId = model.PlantId
            };
            try
            {
                _passportRepository.Add(passport);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: PassportController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            var passport = await _passportRepository.GetByIdAsync(id);
            if (passport == null)
            {
                return NotFound();
            }

            var plant = await _plantRepository.GetAll();
            ViewBag.PlantId = new SelectList(plant, "PlantId", "Name");

            return View(passport);
        }

        // POST: PassportController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, CreatePassportViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.PlantId = new SelectList(await _plantRepository.GetAll(), "PlantId", "Name");
                return View("Edit");
            }

            var passport = await _passportRepository.GetByIdAsync(id);
            if (passport == null)
            {
                return NotFound();
            }

            passport.LatinName = model.LatinName;
            passport.Origin = model.Origin;
            passport.MotherFarm = model.MotherFarm;
            passport.LinkToFloraCroatia = model.LinkToFloraCroatia;
            passport.PlantId = model.PlantId;
            try
            {
                _passportRepository.Update(passport);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: PassportController/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            var passport = await _passportRepository.GetByIdAsync(id);
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
            var passport = await _passportRepository.GetByIdAsync(id);
            if (passport == null)
            {
                // If the infrastructure object is not found, return a 404 error
                return NotFound();
            }

            // Delete the infrastructure object from the database
            _passportRepository.Delete(passport);
            // Redirect to the index page

            // Otherwise, redirect to the Index action in the current controller
            return RedirectToAction("Index");

        }
    }
}
