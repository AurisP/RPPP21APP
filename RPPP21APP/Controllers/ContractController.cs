using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RPPP21APP.Data;
using RPPP21APP.Interfaces;
using RPPP21APP.Models;
using RPPP21APP.Repositories;
using RPPP21APP.Repository;
using RPPP21APP.ViewModels;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace RPPP21APP.Controllers
    {
    public class ContractController : Controller
    {
        private readonly IContractRepository _contractRepository;
        private readonly IContractorRepository _contractorRepository;
   
        public ContractController(IContractRepository contractRepository, IContractorRepository contractorRepository)
        {
            _contractRepository = contractRepository;
            _contractorRepository = contractorRepository;
        }

        // GET: Contract
        public async Task<IActionResult> Index()
        {
            var contracts = await _contractRepository.GetAll();
            return View(contracts);
        }
        
        // GET: Contract/Details/5
        public async Task<IActionResult> Details(int id)
        {
            using (var context = new ApplicationDbContext())
            {
                var contract = await context.Contracts
                    .Include(i => i.Contractor)
                    .Include(i => i.Leases)
                        .ThenInclude(i => i.LeaseType)
                    .Include(i => i.Leases)
                        .ThenInclude(i => i.Plot)
                    .AsNoTracking()
                    .FirstOrDefaultAsync(x => x.ContractId == id);

                return View(contract);
            }
        }

        // GET: Contract/Create
        public async Task<IActionResult> Create()
        {
            ViewBag.ContractorId = new SelectList(await _contractorRepository.GetAll(), "ContractorId", "Surname");
            return View();
        }
        
        

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateContractViewModel contractVM)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.ContractorId = new SelectList(await _contractorRepository.GetAll(), "ContractorId", "Surname");
                return View("Create");
            }

            var contract = new Models.Contract
            {
                Date = (DateTime)contractVM.Date,
                ExpiryDate = (DateTime)contractVM.ExpiryDate,
                Description = contractVM.Description,
                ContractorId = contractVM.ContractorId
            };

            try
            {
                _contractRepository.Add(contract);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Contract/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var contract = await _contractRepository.GetByIdAsync(id);
            if (contract == null)
            {
                return NotFound();
            }

            ViewBag.ContractorId = new SelectList(await _contractorRepository.GetAll(), "ContractorId", "Surname");

            return View(contract);
        }
        
        // POST: Contract/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, CreateContractViewModel contractVM)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.ContractorId = new SelectList(await _contractorRepository.GetAll(), "ContractorId", "Surname");
                return View("Edit");
            }

            var contract = await _contractRepository.GetByIdAsync(id);
            if (contract == null)
            {
                return NotFound();
            }

            //System.Console.WriteLine(contract.ContractorId);
            contract.Date = (DateTime)contractVM.Date;
            contract.ExpiryDate = (DateTime)contractVM.ExpiryDate;
            contract.Description = contractVM.Description;
            contract.ContractorId = contractVM.ContractorId;
            //System.Console.WriteLine(contractVM.ContractorId);
            //System.Console.WriteLine(contract.ContractorId);

            try
            {
                _contractRepository.Update(contract);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        
        // GET: Contract/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var contract = await _contractRepository.GetByIdAsync(id);
            if (contract == null)
            {
                return NotFound();
            }

            return View(contract);
        }

        // POST: Contract/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var contract = await _contractRepository.GetByIdAsync(id);
            if (contract == null)
            {
                return NotFound();
            }
            _contractRepository.Delete(contract);
            return RedirectToAction("Index");
        }
        /*
        private bool ContractExists(int id)
        {
            return _context.Contracts.Any(e => e.ContractId == id);
        }*/
    }
}

