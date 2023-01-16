using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RPPP21APP.Data;
using RPPP21APP.Interfaces;
using RPPP21APP.Models;
using RPPP21APP.Repositories;
using RPPP21APP.Repository;
using RPPP21APP.ViewModels;

namespace RPPP21APP.Controllers
{
    public class GroupOfPlantsController : Controller
    {
        private readonly IGroupOfPlants _groupOfPlantsRepository;
        private readonly IPlotRepository _plotRepository;
        private readonly IPlantTypeRepository _plantTypeRepository;

        public GroupOfPlantsController(IGroupOfPlants groupOfPlantsRepository, IPlotRepository plotRepository
            ,IPlantTypeRepository plantTypeRepository)
        {
            _groupOfPlantsRepository = groupOfPlantsRepository;
            _plotRepository = plotRepository;
            _plantTypeRepository = plantTypeRepository;
        }

        public async Task<IActionResult> Index()
        {
            var groupofplants = await _groupOfPlantsRepository.GetAll();
            return View(groupofplants);
        }

        [HttpGet]
        [Route("Plot/{PlotId}/Group/{TypeId}")]
        public async Task<IActionResult> Detail(int plotId, int typeId)
        {
            var group = await _groupOfPlantsRepository.GetByIdAsyncNoTrack(plotId, typeId);
            if (group == null) return View("Error");

            return View(group);
            
        }


        [HttpGet]       
        public async Task<IActionResult> Create()
        {
            CreateGroupViewModel groupVM = new CreateGroupViewModel();
            //groupVM.GroupsOnPlot.PlotId = plotId;
            groupVM.Plots = await _plotRepository.GetAll();
            groupVM.PlantTypes = await _plantTypeRepository.GetAll();
            return View(groupVM);
        }

        [HttpPost]        
        public async Task<IActionResult> Create(CreateGroupViewModel groupVM)
        {
            var groupOnPlot = new GroupsOnPlot
            {
                PlotId = groupVM.PlotId,
                PlantTime = groupVM.GroupsOnPlot.PlantTime,
                Quantity = groupVM.GroupsOnPlot.Quantity
            };

            var groupOfPlants = new GroupOfPlant
            {
                PlantTypeId = groupVM.PlantTypeId,
                GroupsOnPlot = groupOnPlot
            };

            if (!(_groupOfPlantsRepository.Add(groupOfPlants)))
                return View("Error");
            return RedirectToAction("Index");
        }
    }
}
