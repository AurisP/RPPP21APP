using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RPPP21APP.Data;
using RPPP21APP.Interfaces;
using RPPP21APP.Models;
using RPPP21APP.Repository;
using RPPP21APP.ViewModels;

namespace RPPP21APP.Controllers
{
    public class ActionOnGroupController : Controller
    {
        private readonly IActionOnGroupRepository _actionOnGroupRepository;
        private readonly IWorkerRepository _workerRepository;
        private readonly IMaterialRepository _materialRepository;
        private readonly IMaterialUseRepository _materialUseRepository;
        private readonly ApplicationDbContext _context;

        public ActionOnGroupController(IActionOnGroupRepository actionOnGroupRepository,
            IWorkerRepository workerRepository, IMaterialRepository materialRepository, IMaterialUseRepository materialUseRepository,
            ApplicationDbContext context)
        {
            _actionOnGroupRepository = actionOnGroupRepository;
            _workerRepository = workerRepository;
            _materialRepository = materialRepository;
            _materialUseRepository = materialUseRepository;
            _context = context;
        }
        public async Task<IActionResult> Index(int id)
        {
            var model = await _actionOnGroupRepository.GetListByIdAsync(id);
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            CreateActionOnGroupViewModel actionVM = new CreateActionOnGroupViewModel()
            {
                Workers = await _workerRepository.GetAll(),
                Materials = await _materialRepository.GetAll()
            };           
            return View(actionVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateActionOnGroupViewModel actionVM)
        {
            if (!ModelState.IsValid)
            {
                return View("Create");
            }

            //Compare storage and amount
            if (_materialRepository.GetByIdAsyncNoTrack(actionVM.MaterialId).Result.AmountInStorage < actionVM.materialUse.Amount)
            {
                return View("Create");
            }

            int materialUseId = 0; //initializing id. If no materials used, stays 0;
            if (actionVM.materialUse.Amount > 0)
            {
                var materialUse = new MaterialUse()
                {
                    MaterialId = actionVM.MaterialId,
                    Amount = actionVM.materialUse.Amount,
                };
                materialUseId = _materialUseRepository.Add(materialUse);
                if (materialUseId == -1)//Error in adding at repository file
                {
                    return View("Error");
                }
            }

            if (actionVM.QuantityIfHarvest > 0)
            {
                var storage = new Storage()
                {
                    TimeOfHarvest = actionVM.Time,
                    Amount = actionVM.QuantityIfHarvest,

                    //PlotId = _context.ActionOnGroups
                    //.Include(i => i.GroupOfPlants)
                    //    .ThenInclude(i => i.GroupsOnPlot)
                    //.AsNoTracking()
                    //.FirstOrDefaultAsync(i => i.Plotid)
                    
                };
            }

            var action = new ActionOnGroup()
            {
                Time = actionVM.Time,
                QuantityIfHarvest= actionVM.QuantityIfHarvest,
                WorkerId= actionVM.WorkerId,
                ActionId= actionVM.ActionId,
                MaterialUseId= materialUseId,
                StorageId = actionVM.StorageId,
            };
            _actionOnGroupRepository.Add(action);
            return View();
        }
    }
}
