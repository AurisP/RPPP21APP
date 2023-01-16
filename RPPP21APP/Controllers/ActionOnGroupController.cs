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
        private readonly IStorageRepository _storageRepository;
        private readonly IActionRepository _actionRepository;
        private readonly ApplicationDbContext _context;

        public ActionOnGroupController(IActionOnGroupRepository actionOnGroupRepository,
            IWorkerRepository workerRepository, IMaterialRepository materialRepository, IMaterialUseRepository materialUseRepository,
            IStorageRepository storageRepository, IActionRepository actionRepository, ApplicationDbContext context)
        {
            _actionOnGroupRepository = actionOnGroupRepository;
            _workerRepository = workerRepository;
            _materialRepository = materialRepository;
            _materialUseRepository = materialUseRepository;
            _storageRepository = storageRepository;
            _actionRepository = actionRepository;
            _context = context;
        }

        [Route("ActionOnGroup/{id}")]
        public async Task<IActionResult> Index(int id)
        {
            var model = await _actionOnGroupRepository.GetListByIdAsync(id);
            return View(model);
        }

        [HttpGet]
        [Route("ActionOnGroup/Create/{id}")]
        public async Task<IActionResult> Create(int id)
        {
            CreateActionOnGroupViewModel actionVM = new CreateActionOnGroupViewModel()
            {
                Workers = await _workerRepository.GetAll(),
                Materials = await _materialRepository.GetAll(),               
            };           
            return View(actionVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]       
        public async Task<IActionResult> Create(int id, CreateActionOnGroupViewModel actionVM)
        {

            //Compare storage and amount
            Material material = await _materialRepository.GetByIdAsyncNoTrack(actionVM.MaterialId);
            if (material.AmountInStorage < actionVM.materialUse.Amount)
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

                //Takes amount off from Material storage
                material.AmountInStorage -= actionVM.materialUse.Amount;     
                if(!(_materialRepository.Update(material)))
                    return View("Error");

            }

            if (actionVM.QuantityIfHarvest > 0)
            {
                //Gets PlotId for Storage model
                GroupOfPlant? groupbuf = await _context.GroupOfPlants
                    .Include(i => i.GroupsOnPlot)
                    .FirstOrDefaultAsync(i => i.GroupOfPlantsId == id);

                if (groupbuf == null)
                    return View("Error");

                var storage = new Storage();
                storage.TimeOfHarvest = actionVM.Time;
                storage.Amount = actionVM.QuantityIfHarvest;
                storage.PlotId = groupbuf.GroupsOnPlot.PlotId;
                storage.Place = actionVM.Storage.Place;
                storage.PlantTypeId = groupbuf.PlantTypeId;
                if (!(_storageRepository.Add(storage)))
                    return View("Error");
                actionVM.StorageId = storage.StorageId;
            }

            var action = new ActionM()
            {
            Description = actionVM.ActionM.Description,
            };

            if (!(_actionRepository.Add(action)))
                return View("Error");

            var actionOnGroup = new ActionOnGroup()
            {
                Time = actionVM.Time,
                QuantityIfHarvest= actionVM.QuantityIfHarvest,
                WorkerId= actionVM.WorkerId,                
                MaterialUseId= materialUseId,
                StorageId = actionVM.StorageId,
                GroupOfPlantsId = id,
                ActionId = action.ActionId
            };
            if (!(_actionOnGroupRepository.Add(actionOnGroup)))
                return View("Error");


            return RedirectToAction("index");
        }
    }
}
