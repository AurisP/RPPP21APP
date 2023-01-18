using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using RPPP21APP.Interfaces;
using RPPP21APP.Models;
using RPPP21APP.ViewModels;

namespace RPPP21APP.Controllers
{
    public class LeaseController : Controller
    {
        private readonly ILeaseRepository _leaseRepository;
        private readonly ILeaseTypeRepository _leasetypeRepository;
        private readonly IPlotRepository _plotRepository;
        private readonly IContractRepository _contractRepository;

        public LeaseController(ILeaseRepository leaseRepository, ILeaseTypeRepository leasetypeRepository, IPlotRepository plotRepository, IContractRepository contractRepository)
        {
            _leaseRepository = leaseRepository;
            _leasetypeRepository = leasetypeRepository;
            _plotRepository = plotRepository;
            _contractRepository = contractRepository;
        }

        // GET: Lease
        public async Task<IActionResult> Index()
        {
            var leases = await _leaseRepository.GetAll();
            return View(leases);
        }

        // GET: Lease/Create
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            TempData["PreviousPage"] = HttpContext.Request.Headers["Referer"].ToString();
            ViewBag.ContractId = new SelectList(await _contractRepository.GetAll(), "ContractId", "Description");
            ViewBag.LeaseTypeId = new SelectList(await _leasetypeRepository.GetAll(), "LeaseTypeId", "Name");
            ViewBag.PlotId = new SelectList(await _plotRepository.GetAll(), "PlotId", "Name");
            return View();
        }

        // POST: Lease/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateLeaseViewModel leaseVM)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.ContractId = new SelectList(await _contractRepository.GetAll(), "ContractId", "Description");
                ViewBag.LeaseTypeId = new SelectList(await _leasetypeRepository.GetAll(), "LeaseTypeId", "Name");
                ViewBag.PlotId = new SelectList(await _plotRepository.GetAll(), "PlotId", "Name");
                return View("Create");
            }

            var lease = new Lease
            {
                Cost = leaseVM.Cost,
                ContractId = leaseVM.ContractId,
                LeaseTypeId = leaseVM.LeaseTypeId,
                PlotId = leaseVM.PlotId
            };

            try
            {
                _leaseRepository.Add(lease);
                return Redirect(TempData["PreviousPage"].ToString());
                //return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Lease/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var lease = await _leaseRepository.GetByIdAsync(id);
            if (lease == null)
            {
                return NotFound();
            }

            TempData["PreviousPage"] = HttpContext.Request.Headers["Referer"].ToString();
            ViewBag.ContractId = new SelectList(await _contractRepository.GetAll(), "ContractId", "Description");
            ViewBag.LeaseTypeId = new SelectList(await _leasetypeRepository.GetAll(), "LeaseTypeId", "Name");
            ViewBag.PlotId = new SelectList(await _plotRepository.GetAll(), "PlotId", "Name");

            return View(lease);
        }

        // POST: Lease/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, CreateLeaseViewModel leaseVM)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.ContractId = new SelectList(await _contractRepository.GetAll(), "ContractId", "Description");
                ViewBag.LeaseTypeId = new SelectList(await _leasetypeRepository.GetAll(), "LeaseTypeId", "Name");
                ViewBag.PlotId = new SelectList(await _plotRepository.GetAll(), "PlotId", "Name");
                return View("Edit");
            }

            var lease = await _leaseRepository.GetByIdAsync(id);
            if (lease == null)
            {
                return NotFound();
            }

            //System.Console.WriteLine(contract.ContractorId);
            lease.Cost = leaseVM.Cost;
            lease.ContractId = leaseVM.ContractId;
            lease.LeaseTypeId = leaseVM.LeaseTypeId;
            lease.PlotId = leaseVM.PlotId;
            //System.Console.WriteLine(contractVM.ContractorId);
            //System.Console.WriteLine(contract.ContractorId);

            try
            {
                _leaseRepository.Update(lease);
                return Redirect(TempData["PreviousPage"].ToString());
                //return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Lease/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            TempData["PreviousPage"] = HttpContext.Request.Headers["Referer"].ToString();
            var lease = await _leaseRepository.GetByIdAsync(id);
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
            var lease = await _leaseRepository.GetByIdAsync(id);
            if (lease == null)
            {
                return NotFound();
            }
            _leaseRepository.Delete(lease);
            return Redirect(TempData["PreviousPage"].ToString());
            //return RedirectToAction("Index");
        }
    }
}
