using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QLBanHangDemo.Models;
using QLBanHangDemo.Services.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QLBanHangDemo.Controllers
{
    public class CustomerController : Controller
    {
        // GET: CustomerController
        private readonly ICustomerReponsitory _customerRepository;
        public CustomerController(ICustomerReponsitory customerReponsitory)
        {
           _customerRepository = customerReponsitory;
        }      

        // GET: CustomerController/Details/5
        public IActionResult Index()
        {
            var customers = _customerRepository.GetAll();
            return View(customers);
        }

        public IActionResult Details(int detailId)
        {
            var customers = _customerRepository.GetById(detailId);
            if (customers == null)
            {
                return NotFound();
            }

            return View(customers);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost, ActionName("Create")]
        [ValidateAntiForgeryToken]
        public IActionResult CreatePost(Customers model)
        {
            if (ModelState.IsValid)
            {
                _customerRepository.Add(model);
            }

            return RedirectToAction("Details", new { detailId = model.CustomersId });
        }
        public IActionResult Edit(int id)
        {
            var customers = _customerRepository.GetById(id);
            if (customers == null)
            {
                return NotFound();
            }

            return View(customers);
        }

        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public IActionResult EditPost(Customers model)
        {
            if (ModelState.IsValid)
            {
                _customerRepository.Update(model);
                return RedirectToAction("Details", new { detailId = model.CustomersId });
            }

            return View("Edit");
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var customers = _customerRepository.GetById(id);
            if (customers == null && id == 0)
            {
                return NotFound();
            }

            return View(customers);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(int CustomersId)
        {
            var customers = _customerRepository.GetById(CustomersId);
            if (customers == null )
            {
                return NotFound();
            }

            _customerRepository.Delete(customers);
            return RedirectToAction("Index");
        }
    }
}
