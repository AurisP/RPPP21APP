using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using RPPP21APP.Interfaces;
using RPPP21APP.Models;
using RPPP21APP.Repositories;
using RPPP21APP.ViewModels;

namespace RPPP21APP.Controllers
{
    public class CustomerController : Controller
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerController(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        // GET: CustomerController
        public async Task<IActionResult> Index()
        {
            var customer = await _customerRepository.GetAll();
            return View(customer);
        }

        // GET: CustomerController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: CustomerController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CustomerController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult<Customer>> Create(CreateCustomerViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("Create");
            }
            var customer = new Customer
            {
                Name =  model.Name,
                PhoneNumber = model.PhoneNumber,
                Email = model.Email

            };

            try
            {
                _customerRepository.Add(customer);
                return RedirectToAction("Index");
            }
            catch
            {
                return View("Index");
            }
        }

        // GET: CustomerController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            var customer = await _customerRepository.GetByIdAsync(id);
            if (customer == null)
            {
                return NotFound();
            }
            try
            {
                return View(customer);
            }
            catch
            {
                return View();
            }
        }

        // POST: CustomerController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, CreateCustomerViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("Edit");
            }

            var customer = await _customerRepository.GetByIdAsync(id);
            if (customer == null)
            {
                return NotFound();
            }

            customer.Name = model.Name;
            customer.Email = model.Email;
            customer.PhoneNumber = model.PhoneNumber;

            try
            {
                _customerRepository.Update(customer);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: CustomerController/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            var customer = await _customerRepository.GetByIdAsync(id);
            if (customer == null)
            {
                // If the infrastructure object is not found, return a 404 error
                return NotFound();
            }

            // Pass the infrastructure object to the view
            return View(customer);
        }

        // POST: CustomerController/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            // Get the infrastructure object with the given id
            var customer = await _customerRepository.GetByIdAsync(id);
            if (customer == null)
            {
                // If the infrastructure object is not found, return a 404 error
                return NotFound();
            }

            // Delete the infrastructure object from the database
            _customerRepository.Delete(customer);
            // Redirect to the index page

            // Otherwise, redirect to the Index action in the current controller
            return RedirectToAction("Index");

        }
    }
}
