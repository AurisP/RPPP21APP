using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RPPP21APP.Data;
using RPPP21APP.Interfaces;

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
        [Route("Plot/{PlotId}/{TypeId}")]
        public async Task<IActionResult> Detail(int PlotId, int TypeId)
        {
                var group = await _groupOfPlantsRepository.GetByIdAsyncNoTrack(PlotId, TypeId);

                return View(group);
            
        }
    }
}
