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
using RPPP21APP.Repository;
using RPPP21APP.ViewModels;

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
            return View();
        }
        
        // GET: Contract/Details/5
        /*public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _contractRepository == null)
            {
                return NotFound();
            }

            var contract = await _contractRepository.GetByIdAsync(id.Value);

            if (contract == null)
            {
                return NotFound();
            }

            return View(contract);
        }*/

        // GET: Contract/Create
        public async Task<IActionResult> Create()
        {
            CreateContractViewModel contractVM = new CreateContractViewModel()
            {
                Contractors = await _contractorRepository.GetAll()
            };
            return View(contractVM);
            //return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateContractViewModel contractVM)
        {
            var contract = new Models.Contract
            {
                Date = contractVM.Date,
                ExpiryDate = contractVM.ExpiryDate,
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
        /*public async Task<IActionResult> Edit(int id)
        {
            var contract = await _contractRepository.GetByIdAsync(id);
            if (contract == null) return View("Error");
            var contractVM = new CreateContractViewModel
            {
                ContractId = contractVM.ContractId,
                Date = contractVM.Date,
                ExpiryDate = contractVM.ExpiryDate,
                Description = contractVM.Description,
            };
            return View(contractVM);
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
                ModelState.AddModelError("", "Failed to edit plot");
                return View("Edit", contractVM);
            }

            var contract = new Models.Contract
            {
                ContractId = contractVM.ContractId,
                Date = contractVM.Date,
                ExpiryDate = contractVM.ExpiryDate,
                Description = contractVM.Description,
            };

            var entity = await _contractRepository.GetByIdAsyncNoTrack(id);
            if (entity == null) return View("Error");
            _contractRepository.Update(contract);

            return RedirectToAction("Index");
        }

        // GET: Contract/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Contracts == null)
            {
                return NotFound();
            }

            var contract = await _context.Contracts
                .Include(c => c.Contractor)
                .FirstOrDefaultAsync(m => m.ContractId == id);
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
            if (_context.Contracts == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Contracts'  is null.");
            }
            var contract = await _context.Contracts.FindAsync(id);
            if (contract != null)
            {
                _context.Contracts.Remove(contract);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ContractExists(int id)
        {
            return _context.Contracts.Any(e => e.ContractId == id);
        }*/
    }
}

