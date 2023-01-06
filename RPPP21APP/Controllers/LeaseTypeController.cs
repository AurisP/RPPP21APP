using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RPPP21APP.Data;
using RPPP21APP.Models;

namespace RPPP21APP.Controllers
{
    public class LeaseTypeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public LeaseTypeController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: LeaseType
        public async Task<IActionResult> Index()
        {
              return View(await _context.LeaseTypes.ToListAsync());
        }

        // GET: LeaseType/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.LeaseTypes == null)
            {
                return NotFound();
            }

            var leaseType = await _context.LeaseTypes
                .FirstOrDefaultAsync(m => m.LeaseTypeId == id);
            if (leaseType == null)
            {
                return NotFound();
            }

            return View(leaseType);
        }

        // GET: LeaseType/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: LeaseType/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("LeaseTypeId,Name")] LeaseType leaseType)
        {
            if (ModelState.IsValid)
            {
                _context.Add(leaseType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(leaseType);
        }

        // GET: LeaseType/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.LeaseTypes == null)
            {
                return NotFound();
            }

            var leaseType = await _context.LeaseTypes.FindAsync(id);
            if (leaseType == null)
            {
                return NotFound();
            }
            return View(leaseType);
        }

        // POST: LeaseType/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("LeaseTypeId,Name")] LeaseType leaseType)
        {
            if (id != leaseType.LeaseTypeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(leaseType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LeaseTypeExists(leaseType.LeaseTypeId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(leaseType);
        }

        // GET: LeaseType/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.LeaseTypes == null)
            {
                return NotFound();
            }

            var leaseType = await _context.LeaseTypes
                .FirstOrDefaultAsync(m => m.LeaseTypeId == id);
            if (leaseType == null)
            {
                return NotFound();
            }

            return View(leaseType);
        }

        // POST: LeaseType/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.LeaseTypes == null)
            {
                return Problem("Entity set 'ApplicationDbContext.LeaseTypes'  is null.");
            }
            var leaseType = await _context.LeaseTypes.FindAsync(id);
            if (leaseType != null)
            {
                _context.LeaseTypes.Remove(leaseType);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LeaseTypeExists(int id)
        {
          return _context.LeaseTypes.Any(e => e.LeaseTypeId == id);
        }
    }
}
