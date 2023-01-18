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
        private readonly IPlantRepository _plantRepository;
        public ReservationController(IReservationRepository reservationRepository, IPlantReservationRepository plantReservationRepository, ICustomerRepository customerRepository, IPlantRepository plantRepository)
        {
            _reservationRepository = reservationRepository;
            _plantReservationRepository = plantReservationRepository;
            _customerRepository = customerRepository;
            _plantRepository = plantRepository;
        }
        // GET: ReservationController
        public async Task<IActionResult> Index()
        {
            var reservations = await _reservationRepository.GetAll();
            return View(reservations);
        }

        // GET: ReservationController/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var reservation = await _reservationRepository.GetByIdAsyncNoTrack(id);
            var plantsReservation = await _plantReservationRepository.GetByReservationIdWithPlantAsync(id);
            ViewBag.Reservation = plantsReservation;

            return View(reservation);
        }

        // GET: ReservationController/Create // THIS ONE IS FOR CREATING ORDER ( NOT ADDING PLANTS )
        public async Task<ActionResult> Create()
        {
            ViewBag.CustomerId = new SelectList(await _customerRepository.GetAll(), "CustomerId", "Name");
            return View();
        }
        // This One is to add Apples, Bananas to order that was created higher
        public async Task<ActionResult> AddPlantReservation(int id)
        {
            ViewBag.ReservationId = id;
            ViewBag.PlantId = new SelectList(await _plantRepository.GetAll(), "PlantId", "Name");
            return View();
        }
        // This One is to save Apples, Bananas into the database
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult<Reservation>> AddPlantReservation(CreatePlantReservationViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.ReservationId = model.ReservationId;
                ViewBag.PlantId = new SelectList(await _plantRepository.GetAll(), "PlantId", "Name");
                return View("AddPlantReservation");
            }

            var plantsReservation = new PlantsReservation
            {
                ReservationId = model.ReservationId,
                Price = model.Price,
                Amount = model.Amount,
                PlantId= model.PlantId
            };
            try
            {
                _plantReservationRepository.Add(plantsReservation);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // POST: ReservationController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult<Reservation>> Create(CreateReservationViewModel model)
        {
            
            var reservation = new Reservation
            {
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
        public async Task<ActionResult> Edit(int id)
        {
            var plantreservation = await _plantReservationRepository.GetByIdAsync(id);
            if (plantreservation == null)
            {
                return NotFound();
            }

            var plant = await _plantRepository.GetAll();
            ViewBag.PlantId = new SelectList(plant, "PlantId", "Name");

            return View(plantreservation);
        }

        // POST: ReservationController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, CreatePlantReservationViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.PlantId = new SelectList(await _plantRepository.GetAll(), "PlantId", "Name");
                return View("Edit");
            }

            var plantreservation = await _plantReservationRepository.GetByIdAsync(id);
            if (plantreservation == null)
            {
                return NotFound();
            }

            plantreservation.PlantId = model.PlantId;
            plantreservation.Amount = model.Amount;
            plantreservation.Price= model.Price;
            try
            {
                _plantReservationRepository.Update(plantreservation);
                return RedirectToAction("Details", new{ id = model.ReservationId });
            }
            catch
            {
                return View();
            }
        }

        // GET: ReservationController/Delete/5
        public async Task<ActionResult> DeletePlant(int id)
        {
            var plantreservation = await _plantReservationRepository.GetByIdAsync(id);
            if (plantreservation == null)
            {
                // If the infrastructure object is not found, return a 404 error
                return NotFound();
            }

            // Pass the infrastructure object to the view
            return View(plantreservation);
        }

        // POST: ReservationController/Delete/5
        [HttpPost, ActionName("DeletePlant")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeletePlantConfirmed(int id)
        {
            // Get the infrastructure object with the given id
            var plantreservation = await _plantReservationRepository.GetByIdAsync(id);
            if (plantreservation == null)
            {
                // If the infrastructure object is not found, return a 404 error
                return NotFound();
            }

            // Delete the infrastructure object from the database
            int redirect = plantreservation.ReservationId;
            _plantReservationRepository.Delete(plantreservation);
            // Redirect to the index page

            // Otherwise, redirect to the Index action in the current controller
            return RedirectToAction("Details", new { id = redirect });
        }

        // GET: ReservationController/Delete/5
        public async Task<ActionResult> DeleteWholeReservation(int id)
        {
            var reservation = await _reservationRepository.GetByIdAsync(id);
            if (reservation == null)
            {
                // If the infrastructure object is not found, return a 404 error
                return NotFound();
            }

            // Pass the infrastructure object to the view
            return View(reservation);
        }

        // POST: ReservationController/Delete/5
        [HttpPost, ActionName("DeleteWholeReservation")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteWholeReservationConfirmed(int id)
        {
            // Get the infrastructure object with the given id
            var reservation = await _reservationRepository.GetByIdAsync(id);
            if (reservation == null)
            {
                // If the infrastructure object is not found, return a 404 error
                return NotFound();
            }

            // Delete the infrastructure object from the database
            _reservationRepository.Delete(reservation);
            // Redirect to the index page

            // Otherwise, redirect to the Index action in the current controller
            return View("Index");
        }
    }
}
