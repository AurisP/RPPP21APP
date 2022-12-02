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

        private readonly IPlotRepository _clubRepository;
        private readonly IWeatherConditionsRepository _weatherConditionsRepository;

        public PlotController(IPlotRepository plotRepository, IWeatherConditionsRepository weatherConditionsRepository)
        {
            _clubRepository = plotRepository;
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
            CreatePlotViewModel viewModel = new CreatePlotViewModel()
            {
                WeatherConditions = await _weatherConditionsRepository.GetAll()
            };
            return View(viewModel);
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
    }   
}
