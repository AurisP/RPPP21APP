using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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
    public class ContractorController : Controller
    {
        private readonly IContractorRepository _contractorRepository;

        public ContractorController(IContractorRepository contractorRepository)
        {
            _contractorRepository = contractorRepository;
        }

        // GET: Contractor
        public async Task<IActionResult> Index()
        {
              return View(await _contractorRepository.GetAll());
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
        public async Task<IActionResult> Create(CreateContractorViewModel contractorVM)
        {
            var contractor = new Contractor
            {
                Name = contractorVM.Name,
                Surname = contractorVM.Surname,
                PhoneNumber = contractorVM.PhoneNumber,
                Email = contractorVM.Email,
                Address = contractorVM.Address
            };

            _contractorRepository.Add(contractor);
            return RedirectToAction("Index");
        }

        
        // GET: Contractor/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var contractor = await _contractorRepository.GetByIdAsync(id);
            if (contractor == null)
            {
                return NotFound();
            }

            return View(contractor);
        }

        // POST: Contractor/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, CreateContractorViewModel contractorVM)
        {
            var contractor = await _contractorRepository.GetByIdAsync(id);
            if (contractor == null)
            {
                return NotFound();
            }

            contractor.Name = contractorVM.Name;
            contractor.Surname = contractorVM.Surname;
            contractor.PhoneNumber = contractorVM.PhoneNumber;
            contractor.Email = contractorVM.Email;
            contractor.Address = contractorVM.Address;
            try
            {
                _contractorRepository.Update(contractor);
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
            var contractor = await _contractorRepository.GetByIdAsync(id);
            if (contractor == null)
            {
                return NotFound();
            }

            return View(contractor);
        }

        // POST: Contractor/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var contractor = await _contractorRepository.GetByIdAsync(id);
            if (contractor == null)
            {
                return NotFound();
            }

            _contractorRepository.Delete(contractor);
            return RedirectToAction("Index");
        }

        /*private bool ContractorExists(int id)
        {
          return _context.Contractors.Any(e => e.ContractorId == id);
        }*/
        }
    }
