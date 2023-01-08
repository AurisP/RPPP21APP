using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RPPP21APP.Interfaces;

namespace RPPP21APP.Controllers
{
    public class PlantTypeController : Controller
    {

        private readonly IPlantTypeRepository _plantTypeRepository;
        

        public PlantTypeController(IPlantTypeRepository plantTypeRepository)
        {
            _plantTypeRepository= plantTypeRepository;
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
        public ActionResult Create()
        {
            return View();
        }

        // POST: PlantTypeController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
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
