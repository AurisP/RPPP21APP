using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using RPPP21APP.Interfaces;
using RPPP21APP.Models;
using RPPP21APP.Repositories;
using RPPP21APP.ViewModels;

namespace RPPP21APP.Controllers
{
    public class ReservationController : Controller
    {
        private readonly IReservationRepository _reservationRepository;
        private readonly IPlantReservationRepository _plantReservationRepository;
        private readonly ICustomerRepository _customerRepository;
        public ReservationController(IReservationRepository reservationRepository, IPlantReservationRepository plantReservationRepository, ICustomerRepository customerRepository)
        {
            _reservationRepository = reservationRepository;
            _plantReservationRepository = plantReservationRepository;
            _customerRepository = customerRepository;
        }
        // GET: ReservationController
        public async Task<IActionResult> Index()
        {
            var reservations = await _reservationRepository.GetAll();
            return View(reservations);
        }

        // GET: ReservationController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ReservationController/Create
        public async Task<ActionResult> Create()
        {
            ViewBag.CustomerId = new SelectList(await _customerRepository.GetAll(), "CustomerId", "Name");
            return View();
        }

        // POST: ReservationController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult<Reservation>> Create(CreateReservationViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.CustomerId = new SelectList(await _customerRepository.GetAll(), "CustomerId", "Name");
                return View("Create");
            }

            var reservation = new Reservation
            {
                Amount = (int)model.Ammount,
                AgreedPrice = (int)model.AgreedPrice,
                CustomerId = model.CustomerId
            };
            try
            {
                _reservationRepository.Add(reservation);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: ReservationController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ReservationController/Edit/5
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

        // GET: ReservationController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ReservationController/Delete/5
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
