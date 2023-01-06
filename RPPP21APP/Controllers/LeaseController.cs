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
    public class LeaseController : Controller
    {
        private readonly ApplicationDbContext _context;

        public LeaseController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Lease
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Leases.Include(l => l.Contract).Include(l => l.LeaseType).Include(l => l.Plot);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Lease/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Leases == null)
            {
                return NotFound();
            }

            var lease = await _context.Leases
                .Include(l => l.Contract)
                .Include(l => l.LeaseType)
                .Include(l => l.Plot)
                .FirstOrDefaultAsync(m => m.LeaseId == id);
            if (lease == null)
            {
                return NotFound();
            }

            return View(lease);
        }

        // GET: Lease/Create
        public IActionResult Create()
        {
            ViewData["ContractId"] = new SelectList(_context.Contracts, "ContractId", "ContractId");
            ViewData["LeaseTypeId"] = new SelectList(_context.LeaseTypes, "LeaseTypeId", "LeaseTypeId");
            ViewData["PlotId"] = new SelectList(_context.Plots, "PlotId", "PlotId");
            return View();
        }

        // POST: Lease/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("LeaseId,Cost,ContractId,LeaseTypeId,PlotId")] Lease lease)
        {
            if (ModelState.IsValid)
            {
                _context.Add(lease);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ContractId"] = new SelectList(_context.Contracts, "ContractId", "ContractId", lease.ContractId);
            ViewData["LeaseTypeId"] = new SelectList(_context.LeaseTypes, "LeaseTypeId", "LeaseTypeId", lease.LeaseTypeId);
            ViewData["PlotId"] = new SelectList(_context.Plots, "PlotId", "PlotId", lease.PlotId);
            return View(lease);
        }

        // GET: Lease/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Leases == null)
            {
                return NotFound();
            }

            var lease = await _context.Leases.FindAsync(id);
            if (lease == null)
            {
                return NotFound();
            }
            ViewData["ContractId"] = new SelectList(_context.Contracts, "ContractId", "ContractId", lease.ContractId);
            ViewData["LeaseTypeId"] = new SelectList(_context.LeaseTypes, "LeaseTypeId", "LeaseTypeId", lease.LeaseTypeId);
            ViewData["PlotId"] = new SelectList(_context.Plots, "PlotId", "PlotId", lease.PlotId);
            return View(lease);
        }

        // POST: Lease/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("LeaseId,Cost,ContractId,LeaseTypeId,PlotId")] Lease lease)
        {
            if (id != lease.LeaseId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(lease);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LeaseExists(lease.LeaseId))
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
            ViewData["ContractId"] = new SelectList(_context.Contracts, "ContractId", "ContractId", lease.ContractId);
            ViewData["LeaseTypeId"] = new SelectList(_context.LeaseTypes, "LeaseTypeId", "LeaseTypeId", lease.LeaseTypeId);
            ViewData["PlotId"] = new SelectList(_context.Plots, "PlotId", "PlotId", lease.PlotId);
            return View(lease);
        }

        // GET: Lease/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Leases == null)
            {
                return NotFound();
            }

            var lease = await _context.Leases
                .Include(l => l.Contract)
                .Include(l => l.LeaseType)
                .Include(l => l.Plot)
                .FirstOrDefaultAsync(m => m.LeaseId == id);
            if (lease == null)
            {
                return NotFound();
            }

            return View(lease);
        }

        // POST: Lease/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Leases == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Leases'  is null.");
            }
            var lease = await _context.Leases.FindAsync(id);
            if (lease != null)
            {
                _context.Leases.Remove(lease);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LeaseExists(int id)
        {
          return _context.Leases.Any(e => e.LeaseId == id);
        }
    }
}
