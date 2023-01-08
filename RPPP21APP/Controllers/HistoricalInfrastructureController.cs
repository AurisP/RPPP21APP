using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol.Core.Types;
using RPPP21APP.Data;
using RPPP21APP.Interfaces;
using RPPP21APP.Models;
using RPPP21APP.Repositories;
using RPPP21APP.Repository;
using RPPP21APP.ViewModels;
using System.Collections.Generic;
using System.Net;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace RPPP21APP.Controllers
{
    public class HistoricalInfrastructureController : Controller
    {
        private readonly IHistoricalInfrastructureRepository _historicalInfrastructureRepository;
        private readonly IInfrastructureRepository _infrastructureRepository;
        public HistoricalInfrastructureController(IHistoricalInfrastructureRepository historicalInfrastructureRepository, IInfrastructureRepository infrastructureRepository)
        {
            _historicalInfrastructureRepository = historicalInfrastructureRepository;
            _infrastructureRepository = infrastructureRepository;
        }

        // GET: Worker
        public async Task<IActionResult> Index()
        {
            var historical = await _historicalInfrastructureRepository.GetAll();
            var infrastructure = await _infrastructureRepository.GetAll();
            ViewBag.InfrastructureId = new SelectList(infrastructure, "InfrastructureId", "Name");
            return View(historical);
        }

        // GET: HistoricalInfrastructureController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: HistoricalInfrastructureController/Create
        public ActionResult Create(int id)
        {
            // Create a view model for the create form
            var model = new HistoricalInfrastructure
            {
                InfrastructureId = id
            };
            return View(model);
        }

        // POST: HistoricalInfrastructureController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(HistoricalInfrastructure model)
        {
            var historical = new HistoricalInfrastructure
            {
                DateOfdestrcution = (DateTime)model.DateOfdestrcution,
                ReasonOfDestruction = model.ReasonOfDestruction,
                CostOfDestruction = (int?)model.CostOfDestruction,
                EarningsOnMaterials = (int?)model.EarningsOnMaterials,
                InfrastructureId = model.InfrastructureId
            };
            
            try
            {
                _historicalInfrastructureRepository.Add(historical);
                return RedirectToAction("Index", "Infrastructure");
            }
            catch
            {
                return View();
            }
        }

        // GET: HistoricalInfrastructureController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: HistoricalInfrastructureController/Edit/5
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

        // GET: HistoricalInfrastructureController/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            // Get the infrastructure object with the given id
            var historicalinfrastructure = await _historicalInfrastructureRepository.GetByIdAsync(id);
            if (historicalinfrastructure == null)
            {
                // If the infrastructure object is not found, return a 404 error
                return NotFound();
            }
            var infrastructure = await _infrastructureRepository.GetAll();
            ViewBag.InfrastructureId = new SelectList(infrastructure, "InfrastructureId", "Name");
            // Pass the infrastructure object to the view
            return View(historicalinfrastructure);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            // Get the infrastructure object with the given id
            var infrastructure = await _historicalInfrastructureRepository.GetByIdAsync(id);
            if (infrastructure == null)
            {
                // If the infrastructure object is not found, return a 404 error
                return NotFound();
            }

            // Delete the infrastructure object from the database
            _historicalInfrastructureRepository.Delete(infrastructure);
            // Redirect to the index page
            return RedirectToAction("Index");
        }
    }
}
