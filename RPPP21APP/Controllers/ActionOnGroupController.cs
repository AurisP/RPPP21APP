using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RPPP21APP.Data;
using RPPP21APP.Interfaces;
using RPPP21APP.Models;
using RPPP21APP.Repository;
using RPPP21APP.ViewModels;
using System;

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

        //id is groupofplantsid
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


            return RedirectToAction("index", new { id });
        }

        [HttpGet]
        [Route("ActionOnGroup/Details/{Id}")]
        public async Task<IActionResult> Detail(int id)
        {
                var actionOnGroup = await _actionOnGroupRepository.GetByIdAsyncNoTrack(id);

                return View(actionOnGroup);
            
        }

        public async Task<ActionResult> Delete(int id)
        {
            var action = await _actionOnGroupRepository.GetByIdAsync(id);
            if (action == null)
            {
                return NotFound();
            }

            return View(action);
        }

        // POST: WorkersController/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            var actionOnGroup = await _actionOnGroupRepository.GetByIdAsync(id);
            var action = await _actionRepository.GetByIdAsync(actionOnGroup.ActionId);
            var materialUse = await _materialUseRepository.GetByIdAsync(actionOnGroup.MaterialUseId);
            var storage = await _storageRepository.GetByIdAsync(actionOnGroup.StorageId);
            _storageRepository.Delete(storage);
            _actionRepository.Delete(action);
            _materialUseRepository.Delete(materialUse);
            _actionOnGroupRepository.Delete(actionOnGroup);
           
            return RedirectToAction("index" , new { id = actionOnGroup.GroupOfPlantsId} );
        }

        public async Task<IActionResult> Edit(int id)
        {
            ActionOnGroup actionOnGroup = await _actionOnGroupRepository.GetByIdAsyncNoTrack(id);

            CreateActionOnGroupViewModel actionVM = new CreateActionOnGroupViewModel()
            {
                GroupOfPlantsId = actionOnGroup.GroupOfPlantsId,
                Time = actionOnGroup.Time,
                QuantityIfHarvest= actionOnGroup.QuantityIfHarvest,
                ActionId = actionOnGroup.ActionId,
                StorageId = actionOnGroup.StorageId,
                Storage = actionOnGroup.Storage,
                ActionM = actionOnGroup.Action,
                MaterialUseId= actionOnGroup.MaterialUseId,
                materialUse =actionOnGroup.MaterialUse,

                Workers = await _workerRepository.GetAll(),
                Materials = await _materialRepository.GetAll(),
            };
            return View(actionVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, CreateActionOnGroupViewModel actionVM)
        {
            if (!ModelState.IsValid)
            {
                //ViewBag.ContractorId = new SelectList(await _contractorRepository.GetAll(), "ContractorId", "Surname");
                return View("Error");
            }

            //var action = await _actionOnGroupRepository.GetByIdAsync(id);
            //if (action == null)
            //{
            //    return NotFound();
            //}

            ////Compare storage and amount
            //Material material = await _materialRepository.GetByIdAsyncNoTrack(actionVM.MaterialId);
            //if (material.MaterialId != actionVM.MaterialId)

            //int materialUseId = 0; //initializing id. If no materials used, stays 0;
            //if (actionVM.materialUse.Amount > 0)
            //{
            //    var materialUse = new MaterialUse()
            //    {
            //        MaterialId = actionVM.MaterialId,
            //        Amount = actionVM.materialUse.Amount,
            //    };
            //    materialUseId = _materialUseRepository.Add(materialUse);
            //    if (materialUseId == -1)//Error in adding at repository file
            //    {
            //        return View("Error");
            //    }

            //    //Takes amount off from Material storage
            //    material.AmountInStorage -= actionVM.materialUse.Amount;
            //    if (!(_materialRepository.Update(material)))
            //        return View("Error");
            //}

            //if (actionVM.QuantityIfHarvest > 0)
            //{
            //    //Gets PlotId for Storage model
            //    GroupOfPlant? groupbuf = await _context.GroupOfPlants
            //        .Include(i => i.GroupsOnPlot)
            //        .FirstOrDefaultAsync(i => i.GroupOfPlantsId == id);

            //    if (groupbuf == null)
            //        return View("Error");

            //    var storage = new Storage();
            //    storage.TimeOfHarvest = actionVM.Time;
            //    storage.Amount = actionVM.QuantityIfHarvest;
            //    storage.PlotId = groupbuf.GroupsOnPlot.PlotId;
            //    storage.Place = actionVM.Storage.Place;
            //    storage.PlantTypeId = groupbuf.PlantTypeId;
            //    if (!(_storageRepository.Add(storage)))
            //        return View("Error");
            //    actionVM.StorageId = storage.StorageId;
            //}

            //var action = new ActionM()
            //{
            //    Description = actionVM.ActionM.Description,
            //};

            //if (!(_actionRepository.Add(action)))
            //    return View("Error");

            //var actionOnGroup = new ActionOnGroup()
            //{
            //    Time = actionVM.Time,
            //    QuantityIfHarvest = actionVM.QuantityIfHarvest,
            //    WorkerId = actionVM.WorkerId,
            //    MaterialUseId = materialUseId,
            //    StorageId = actionVM.StorageId,
            //    GroupOfPlantsId = id,
            //    ActionId = action.ActionId
            //};
            //if (!(_actionOnGroupRepository.Add(actionOnGroup)))
            //    return View("Error");

            //try
            //{
            //    _actionOnGroupRepository.Update(action);
            //    return RedirectToAction("Index");
            //}
            //catch
            //{
                return View();
            //}
        }

    }
}
