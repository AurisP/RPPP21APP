using Microsoft.AspNetCore.Mvc;
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
            IEnumerable<Plot> plots = await _clubRepository.GetAll();
            return View(plots);
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
        public IActionResult Create(CreatePlotViewModel plotVM)
        {
            if (ModelState.IsValid)
            {
                var plot = new Plot
                {
                    Name = plotVM.Name,
                    Coordinates = plotVM.Coordinates,
                    Area = plotVM.Area,
                    WeatherConditionsId = plotVM.WeatherConditionsId,
                    
                };
                _clubRepository.Add(plot);
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("", "Club creation failed");
            }
            return View(plotVM);
        }
    }

    
}
