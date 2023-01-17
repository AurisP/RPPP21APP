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
            int? materialUseId = null;
            int? storageId = null;
            if (actionVM.MaterialId != 0)
            {
                Material material = await _materialRepository.GetByIdAsyncNoTrack(actionVM.MaterialId);
                if (material.AmountInStorage < actionVM.materialUse.Amount)
                {
                    return View("Create");
                }

                 //initializing id. If no materials used, stays 0;
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
                    if (!(_materialRepository.Update(material)))
                        return View("Error");

                }
            }

            if (actionVM.QuantityIfHarvest > 0 && actionVM.Storage.Place != null)
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
                storageId = storage.StorageId;
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
                StorageId = storageId,
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
            
            //ActionOnGroups delete set to cascade in DB. Deletes also action, storage and materialuse         
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
            //if (!ModelState.IsValid)
            //{
            //    
            //}

            var actionOnGroup = await _actionOnGroupRepository.GetByIdAsync(id);
            if (actionOnGroup == null)
            {
                return NotFound();
            }
           
            if (actionOnGroup.MaterialUse.Material.MaterialId != actionVM.MaterialId)
            {
                if (actionVM.materialUse.Amount > 0)
                {
                    actionOnGroup.MaterialUseId = actionOnGroup.MaterialUseId;
                    actionOnGroup.MaterialUse.MaterialId = actionVM.MaterialId;
                    actionOnGroup.MaterialUse.Amount = actionVM.materialUse.Amount;
                   

                    int bufMaterial = _materialUseRepository.Update(actionOnGroup.MaterialUse);
                    if (bufMaterial == -1)//Error in updating at repository file
                    {
                        return View("Error");
                    }
                    //From here localMaterialUseId is correct id for table                    
                }
                else
                {
                    //if amount is turned to zero, then materialuse is deleted
                    _materialUseRepository.Delete(actionOnGroup.MaterialUse);
                }
            }

            //Adds or deducts the difference between 2 amounts.
            if (actionVM.materialUse.Amount != actionOnGroup.MaterialUse.Amount)
            {
                actionOnGroup.MaterialUse.Material.AmountInStorage -= (actionVM.materialUse.Amount - actionOnGroup.MaterialUse.Amount);
                if (!(_materialRepository.Update(actionOnGroup.MaterialUse.Material)))
                    return View("Error");
            }

            
            if (actionVM.QuantityIfHarvest != actionOnGroup.QuantityIfHarvest)
            {
                
                actionOnGroup.Storage.TimeOfHarvest = actionVM.Time;
                actionOnGroup.Storage.Amount = actionVM.QuantityIfHarvest;
                actionOnGroup.Storage.Place = actionVM.Storage.Place;

                if (!(_storageRepository.Update(actionOnGroup.Storage)))
                    return View("Error");
                
            }

            //Action update
            actionOnGroup.Action.Description = actionVM.ActionM.Description;
            if (!(_actionRepository.Update(actionOnGroup.Action)))
                return View("Error");

            //Group update
            actionOnGroup.Time = actionVM.Time;
            actionOnGroup.QuantityIfHarvest = actionVM.QuantityIfHarvest;
            actionOnGroup.WorkerId = actionVM.WorkerId;
                    
            try
            {
                _actionOnGroupRepository.Update(actionOnGroup);
                return RedirectToAction("Index", new { id = actionOnGroup.GroupOfPlantsId });
            }
            catch
            {
                return View("Error");
            }
        }

    }
}
