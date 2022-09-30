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
    public class PaymentController : Controller
    {
        private readonly IPaymentReponsitory _paymentRepository;
        public PaymentController(IPaymentReponsitory paymentReponsitory)
        {
            _paymentRepository = paymentReponsitory;
        }
        // GET: PaymentController
        public IActionResult Index()
        {
            var payment = _paymentRepository.GetAll();
            return View(payment);
        }

        public IActionResult Details(int detailId)
        {
            var payment = _paymentRepository.GetById(detailId);
            if (payment == null)
            {
                return NotFound();
            }

            return View(payment);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost, ActionName("Create")]
        [ValidateAntiForgeryToken]
        public IActionResult CreatePost(Payment model)
        {
            if (ModelState.IsValid)
            {
                _paymentRepository.Add(model);
            }

            return RedirectToAction("Details", new { detailId = model.PaymentId });
        }
        public IActionResult Edit(int id)
        {
            var payment = _paymentRepository.GetById(id);
            if (payment == null)
            {
                return NotFound();
            }

            return View(payment);
        }

        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public IActionResult EditPost(Payment model)
        {
            if (ModelState.IsValid)
            {
                _paymentRepository.Update(model);
                return RedirectToAction("Details", new { detailId = model.PaymentId });
            }

            return View("Edit");
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var payment = _paymentRepository.GetById(id);
            if (payment == null && id == 0)
            {
                return NotFound();
            }

            return View(payment);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(int PaymentId)
        {
            var payment = _paymentRepository.GetById(PaymentId);
            if (payment == null)
            {
                return NotFound();
            }

            _paymentRepository.Delete(payment);
            return RedirectToAction("Index");
        }
    }
}
