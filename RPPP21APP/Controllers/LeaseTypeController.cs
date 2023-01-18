using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
    public class LeaseTypeController : Controller
    {
        private readonly ILeaseTypeRepository _leasetypeRepository;

        public LeaseTypeController(ILeaseTypeRepository leasetypeRepository)
        {
            _leasetypeRepository = leasetypeRepository;
        }

        // GET: Contractor
        public async Task<IActionResult> Index()
        {
            return View(await _leasetypeRepository.GetAll());
        }


        // GET: Contractor/Create
        public IActionResult Create()
        {
            return View();
        }


        // POST: Contractor/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateLeaseTypeViewModel leasetypeVM)
        {
            if (!ModelState.IsValid)
            {
                return View("Create");
            }

            var leasetype = new LeaseType
            {
                Name = leasetypeVM.Name
            };

            _leasetypeRepository.Add(leasetype);
            return RedirectToAction("Index");
        }


        // GET: Contractor/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var leasetype = await _leasetypeRepository.GetByIdAsync(id);
            if (leasetype == null)
            {
                return NotFound();
            }

            return View(leasetype);
        }

        // POST: Contractor/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, CreateLeaseTypeViewModel leasetypeVM)
        {
            if (!ModelState.IsValid)
            {
                return View("Edit");
            }

            var leasetype = await _leasetypeRepository.GetByIdAsync(id);
            if (leasetype == null)
            {
                return NotFound();
            }

            leasetype.Name = leasetypeVM.Name;



            try
            {
                _leasetypeRepository.Update(leasetype);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }


        // GET: Contractor/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var leasetype = await _leasetypeRepository.GetByIdAsync(id);
            if (leasetype == null)
            {
                return NotFound();
            }

            return View(leasetype);
        }

        // POST: Contractor/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var leasetype = await _leasetypeRepository.GetByIdAsync(id);
            if (leasetype == null)
            {
                return NotFound();
            }

            _leasetypeRepository.Delete(leasetype);
            return RedirectToAction("Index");
        }
    }
}
