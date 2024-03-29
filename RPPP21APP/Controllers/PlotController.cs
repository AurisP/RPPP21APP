﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RPPP21APP.Data;
using RPPP21APP.Interfaces;
using RPPP21APP.Models;
using RPPP21APP.Repositories;
using RPPP21APP.Repository;
using RPPP21APP.ViewModels;
using System.Collections.Generic;
using System.Net;


namespace RPPP21APP.Controllers
{
    public class PlotController : Controller
    {

        private readonly IPlotRepository _plotRepository;
        private readonly IWeatherConditionsRepository _weatherConditionsRepository;

        public PlotController(IPlotRepository plotRepository, IWeatherConditionsRepository weatherConditionsRepository)
        {
            _plotRepository = plotRepository;
            _weatherConditionsRepository = weatherConditionsRepository;
        }
        public async Task<IActionResult> Index()
        {          
                var model = await _plotRepository.GetAll();
                return View(model);            
        }

        [HttpGet]
        [Route("Plot/Create")]
        public async Task<IActionResult> Create()
        {
            CreatePlotViewModel plotVM = new CreatePlotViewModel()
            {
                WeatherConditions = await _weatherConditionsRepository.GetAll()
            };
            return View(plotVM);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreatePlotViewModel plotVM)
        {
            /*if (!ModelState.IsValid)
            {
                plotVM.WeatherConditions = await _weatherConditionsRepository.GetAll();
                return View("Create");
            }*/

            var plot = new Plot
            {
                Name = plotVM.Name,
                Coordinates = plotVM.Coordinates,
                Area = plotVM.Area,
                WeatherConditionsId = plotVM.WeatherConditionsId,
            };

            _plotRepository.Add(plot);               
            return RedirectToAction("Index");           
        }

        [HttpGet]
        [Route("Plot/Edit/{Id}")]
        public async Task<IActionResult> Edit(int id)
        {           
                var plot = await _plotRepository.GetByIdAsync(id);
                if (plot == null) return View("Error");
                var plotVM = new CreatePlotViewModel
                {
                    Name = plot.Name,
                    Coordinates = plot.Coordinates,
                    Area = plot.Area,
                    WeatherConditionsId = plot.WeatherConditionsId,
                    WeatherConditions = await _weatherConditionsRepository.GetAll()
                };
                return View(plotVM);           
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, CreatePlotViewModel plotVM)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Failed to edit plot");
                return View("Edit", plotVM);
            }

            var plot = new Plot
            {
                Name = plotVM.Name,
                Coordinates = plotVM.Coordinates,
                Area = plotVM.Area,
                WeatherConditionsId = plotVM.WeatherConditionsId,
                PlotId = id,
                WeatherConditions = await _weatherConditionsRepository.GetByIdAsync(id),
            };
           
            var entity = await _plotRepository.GetByIdAsyncNoTrack(id);
            if (entity == null) return View("Error");
            _plotRepository.Update(plot);               
            //context.Entry(entity).Property(x => x.PlotId).IsModified = false; //Maybe necessary for safety??
            //context.Entry(entity).CurrentValues.SetValues(plot);                
            //context.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpGet]
        [Route("Plot/{Id}")]
        public async Task<IActionResult> Detail(int id)
        {
            using (var context = new ApplicationDbContext())
            {               
                var plot = await context.Plots.Include(x => x.Infrastructures)
                    .Include(x => x.GroupsOnPlots)
                        .ThenInclude(x => x.GroupOfPlants)
                            .ThenInclude(x => x.PlantType)
                                
                    .Include(x => x.GroupsOnPlots)
                        .ThenInclude(x => x.GroupOfPlants)
                            .ThenInclude(x => x.Plants)

                    .Include(x => x.WeatherConditions)

                    .AsNoTracking()
                    .FirstOrDefaultAsync(x => x.PlotId == id);

                return View(plot);
            }
        }

        public async Task<ActionResult> Delete(int id)
        {
            // Get the object with the given id
            var plot = await _plotRepository.GetByIdAsync(id);
            if (plot == null)
            {
                // If the infrastructure object is not found, return a 404 error
                return NotFound();
            }

            // Pass the infrastructure object to the view
            return View(plot);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            // Get the infrastructure object with the given id
            var plot = await _plotRepository.GetByIdAsync(id);
            if (plot == null)
            {
                // If the infrastructure object is not found, return a 404 error
                return NotFound();
            }

            // Delete the object from the database
            _plotRepository.Delete(plot);
            // Redirect to the index page

            // Otherwise, redirect to the Index action in the current controller
            return RedirectToAction("Index");

        }
    }
}
