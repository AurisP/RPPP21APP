using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol.Core.Types;
using RPPP21APP.Data;
using RPPP21APP.Interfaces;
using RPPP21APP.Models;
using RPPP21APP.Repositories;
using RPPP21APP.Repository;
using RPPP21APP.ViewModels;
using System.Collections.Generic;
using System.Net;

namespace RPPP21APP.Controllers
{
    public class WorkersController : Controller
    {
        

        private readonly IWorkerRepository _workerRepository;

        public WorkersController(IWorkerRepository workerRepository)
        {
            _workerRepository = workerRepository;
        }

        // GET: Worker
        public async Task<IActionResult> Index()
        {
            ;
            var workers = await _workerRepository.GetAll();
            return View(workers);
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
        public IActionResult Create()
        {
            return View();
        }

        // POST: WorkersController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: WorkersController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: WorkersController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: WorkersController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: WorkersController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
