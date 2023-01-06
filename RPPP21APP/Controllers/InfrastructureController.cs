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
using System.Linq;

namespace RPPP21APP.Controllers
{
    public class InfrastructureController : Controller
    {
        // GET: InfrastructureController
        private readonly IInfrastructureRepository _infrastructureRepository;
        private readonly IPlotRepository _plotRepository;
        private readonly IHistoricalInfrastructureRepository _historicalInfrastructureRepository;

        public InfrastructureController(IInfrastructureRepository repository, IPlotRepository plotRepository, IHistoricalInfrastructureRepository historicalInfrastructureRepository)
        {
            _infrastructureRepository = repository;
            _plotRepository = plotRepository;
            _historicalInfrastructureRepository = historicalInfrastructureRepository;
        }
        public async Task<ActionResult> Index()
        {
            //var infrastructures = await _infrastructureRepository.GetAll();
            var infrastructures = await _infrastructureRepository.GetAll();
            var historicalinfrastructures = await _historicalInfrastructureRepository.GetAll();
            var infrastructuresNotInHistoricalInfrastructure = infrastructures.Where(i => !historicalinfrastructures.Any(hi => hi.InfrastructureId == i.InfrastructureId));
            return View(infrastructuresNotInHistoricalInfrastructure);
        }

        // GET: InfrastructureController/Details/5
        public async Task<ActionResult> Details(int id)
        {
            var infrastructure = await _infrastructureRepository.GetByIdAsync(id);
            var historical = await _historicalInfrastructureRepository.GetAll();
            //var historical = (await _historicalInfrastructureRepository.GetAll()).Where(h => h.InfrastructureId == id);
            //ViewBag.DateOfDestruction = historical.Select(h => h.DateOfDestruction);
            //ViewBag.ReasonOfDestruction = historical.Select(h => h.ReasonOfDestruction);
            //ViewBag.CostOfDestruction = historical.Select(h => h.CostOfDestruction);
            //ViewBag.EarningsOnMaterials = historical.Select(h => h.EarningsOnMaterials);

            return View(infrastructure);
        }

        // GET: InfrastructureController/Create
        public async Task<ActionResult> Create()
        {
            ViewBag.PlotId = new SelectList(await _plotRepository.GetAll(), "PlotId", "Name");
            return View();
        }

        // POST: InfrastructureController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateInfrastructureViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var infrastructure = new Infrastructure
            {
                Name = model.Name,
                BuildDate = (DateTime)model.BuildDate,
                Cost = (int?)model.Cost,
                PlotId = model.PlotId
            };
            try
            {
                _infrastructureRepository.Add(infrastructure);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: InfrastructureController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            var infrastructure = await _infrastructureRepository.GetByIdAsync(id);
            if (infrastructure == null)
            {
                return NotFound();
            }

            var plots = await _plotRepository.GetAll();
            ViewBag.PlotId = new SelectList(plots, "PlotId", "Name");

            return View(infrastructure);
        }

        // POST: InfrastructureController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, CreateInfrastructureViewModel model)
        {
            var infrastructure = await _infrastructureRepository.GetByIdAsync(id);
            if (infrastructure == null)
            {
                return NotFound();
            }

            infrastructure.Name = model.Name;
            infrastructure.BuildDate = (DateTime)model.BuildDate;
            infrastructure.Cost = (int?)model.Cost;
            infrastructure.PlotId = model.PlotId;
            try
            {
                _infrastructureRepository.Update(infrastructure);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public async Task<ActionResult> Delete(int id)
        {
            // Get the infrastructure object with the given id
            var infrastructure = await _infrastructureRepository.GetByIdAsync(id);
            if (infrastructure == null)
            {
                // If the infrastructure object is not found, return a 404 error
                return NotFound();
            }

            // Pass the infrastructure object to the view
            return View(infrastructure);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            // Get the infrastructure object with the given id
            var infrastructure = await _infrastructureRepository.GetByIdAsync(id);
            if (infrastructure == null)
            {
                // If the infrastructure object is not found, return a 404 error
                return NotFound();
            }

            // Delete the infrastructure object from the database
            _infrastructureRepository.Delete(infrastructure);
            // Redirect to the index page

                // Otherwise, redirect to the Index action in the current controller
            return RedirectToAction("Index");

        }




    }
}
