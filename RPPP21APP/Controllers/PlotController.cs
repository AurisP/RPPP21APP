using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RPPP21APP.Data;
using RPPP21APP.Interfaces;
using RPPP21APP.Models;
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
            _plotRepository = plotRepository; //Probably not necessary because using DbContext
            _weatherConditionsRepository = weatherConditionsRepository;
        }
        public async Task<IActionResult> Index()
        {
            using (var context = new ApplicationDbContext())
            {
                var model = await context.Plots.Include(a => a.WeatherConditions).AsNoTracking().ToListAsync();
                return View(model);
            }
        }

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
            using (var context = new ApplicationDbContext())
            {
                var plot = new Plot
                {
                    Name = plotVM.Name,
                    Coordinates = plotVM.Coordinates,
                    Area = plotVM.Area,
                    WeatherConditionsId = plotVM.WeatherConditionsId,
                };

                context.Plots.Add(plot);
                await context.SaveChangesAsync();
                return RedirectToAction("Index");
            }

        }

        public async Task<IActionResult> Edit(int id)
        {
            using (var context = new ApplicationDbContext())
            {
                var entity = context.Plots.FirstOrDefault(item => item.PlotId == id);
                if (entity == null) return View("Error");
                var plotVM = new CreatePlotViewModel
                {
                    Name = entity.Name,
                    Coordinates = entity.Coordinates,
                    Area = entity.Area,
                    WeatherConditionsId = entity.WeatherConditionsId,
                    WeatherConditions = await _weatherConditionsRepository.GetAll()
                };
                return View(plotVM);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, CreatePlotViewModel plotVM)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Failed to edit plot");
                return View("Edit", plotVM);
            }
            using (var context = new ApplicationDbContext())
            {
                var plot = new Plot
                {
                    Name = plotVM.Name,
                    Coordinates = plotVM.Coordinates,
                    Area = plotVM.Area,
                    WeatherConditionsId = plotVM.WeatherConditionsId,
                    PlotId = id
                   
                };
                var entity = await context.Plots.FirstOrDefaultAsync(x => x.PlotId == id);
                if (entity == null) return View("Error");
                context.Plots.Update(entity);               
                //context.Entry(entity).Property(x => x.PlotId).IsModified = false; //Maybe necessary for safety??
                context.Entry(entity).CurrentValues.SetValues(plot);                
                context.SaveChanges();

                return RedirectToAction("Index");
            }

        }

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
                            .ThenInclude(x => x.PlantType)

                    .AsNoTracking()
                    .FirstOrDefaultAsync(x => x.PlotId == id);

                return View(plot);
            }
        }
    }
}
