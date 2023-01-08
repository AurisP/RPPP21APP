using Azure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.VisualBasic;
using NuGet.Protocol.Core.Types;
using RPPP21APP.Data;
using RPPP21APP.Interfaces;
using RPPP21APP.Models;
using RPPP21APP.Repositories;
using RPPP21APP.Repository;
using RPPP21APP.ViewModels;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Net;
using static System.Reflection.Metadata.BlobBuilder;

namespace RPPP21APP.Controllers
{
    public class WorkersController : Controller
    {
        
        private readonly IWorkerRepository _workerRepository;
        private readonly AppSettings _appSettings;

        public WorkersController(IWorkerRepository workerRepository, IOptionsSnapshot<AppSettings> options)
        {
            _workerRepository = workerRepository;
            _appSettings = options.Value;
        }

        // GET: Worker
        public async Task<IActionResult> Index(int page = 1, int sort = 1, bool ascending = true)
        {
            int pagesize = _appSettings.PageSize;

            int count = await _workerRepository.GetCountAsync();

            //if (count == 0)
            //{
            //    string message = "There is no worker in the database";
            //    logger.LogInformation(message);
            //    TempData[Constants.Message] = "message";
            //    TempData[Constants.ErrorOccurred] = false;
            //    return RedirectToAction(nameof(Create));
            //}

            var pagingInfo = new PagingInfo
            {
                CurrentPage = page,
                Sort = sort,
                Ascending = ascending,
                ItemsPerPage = pagesize,
                TotalItems = count
            };
            if (page < 1 || page > pagingInfo.TotalPages)
            {
                return RedirectToAction(nameof(Index), new { page = 1, sort, ascending });
            }         

            var workers = await _workerRepository.GetSliceAsync((page - 1) * pagesize, pagesize);

            var workerViewModel = new IndexWorkerViewModel
            {
                Workers = workers,
                Page = page,
                PageSize = pagesize,
                TotalWorkers = count,
                TotalPages = (int)Math.Ceiling(count / (double)pagesize),
                
            };

            return View(workerViewModel);

            //var workers = await _workerRepository.GetAll();
            //return View(workers);
        }

        // GET: Worker/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var worker = await _workerRepository.GetByIdAsync(id.Value);
            if (worker == null)
            {
                return NotFound();
            }

            return View(worker);
        }


        // GET: WorkersController/Create
        // GET: Worker/Create

        public async Task<IActionResult> Create()
        {
            return View();
        }

        // POST: WorkersController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateWorkerViewModel workerVM)
        {
            if (!ModelState.IsValid)
            {
                return View("Create");
            }

            var worker = new Worker
            {
                Name = workerVM.Name,
                Surname = workerVM.Surname,
                Salary = workerVM.Salary,
                PhoneNumber = workerVM.PhoneNumber,
                Experience = workerVM.Experience,
                WorkingHours = workerVM.WorkingHours,
            };

            try
            {
                _workerRepository.Add(worker);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: WorkersController/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var worker = await _workerRepository.GetByIdAsync(id);

            if (worker == null)
            {
                return NotFound();
            }

            return View(worker);
        }

        // POST: WorkersController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, CreateWorkerViewModel workerVM)
        {

            if (!ModelState.IsValid)
            {
                return View("Edit");
            }
            
            var worker = await _workerRepository.GetByIdAsync(id);
            if (worker == null)
            {
                return NotFound();
            }
            try
            {
                // Update worker with values from view model
                worker.Name = workerVM.Name;
                worker.Surname = workerVM.Surname;
                worker.Salary = workerVM.Salary;
                worker.PhoneNumber = workerVM.PhoneNumber;
                worker.Experience = workerVM.Experience;
                worker.WorkingHours = workerVM.WorkingHours;

                _workerRepository.Update(worker);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }




        // GET: WorkersController/Delete/5
        // GET: WorkersController/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            var worker = await _workerRepository.GetByIdAsync(id);
            if (worker == null)
            {
                return NotFound();
            }

            return View(worker);
        }

        // POST: WorkersController/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            var worker = await _workerRepository.GetByIdAsync(id);
            _workerRepository.Delete(worker);
            _workerRepository.Save();
            return RedirectToAction(nameof(Index));
        }
    }
}
