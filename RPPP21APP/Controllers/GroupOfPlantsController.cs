using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RPPP21APP.Data;
using RPPP21APP.Interfaces;
using RPPP21APP.Models;
using RPPP21APP.Repository;
using RPPP21APP.ViewModels;

namespace RPPP21APP.Controllers
{
    public class GroupOfPlantsController : Controller
    {
        private readonly IGroupOfPlants _groupOfPlantsRepository;

        public GroupOfPlantsController(IGroupOfPlants groupOfPlantsRepository)
        {
            _groupOfPlantsRepository = groupOfPlantsRepository;
        }

        public IActionResult Index()
        {
            return View();
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
        [Route("Plot/{PlotId}/Group/Create")]
        public async Task<IActionResult> Create(int plotId)
        {
            CreateGroupViewModel groupVM = new CreateGroupViewModel();
            groupVM.GroupsOnPlot.PlotId = plotId;
            groupVM.PlantTypeList = await _groupOfPlantsRepository.GetAvailableTypes(plotId);
            return View(groupVM);
        }

        [HttpPost]
        [Route("Plot/{PlotId}/Group/Create")]
        public async Task<IActionResult> Create(CreateGroupViewModel groupVM)
        {
            var groupOnPlot = new GroupsOnPlot
            {
                PlotId = groupVM.GroupsOnPlot.PlotId,
                PlantTime = groupVM.GroupsOnPlot.PlantTime,
                Quantity = groupVM.GroupsOnPlot.Quantity
            };

            var groupOfPlants = new GroupOfPlant
            {
                PlantTypeId = groupVM.PlantTypeId,
                GroupsOnPlot = groupOnPlot
            };

            _groupOfPlantsRepository.Add(groupOfPlants);
            return RedirectToAction("Index");
        }
    }
}
