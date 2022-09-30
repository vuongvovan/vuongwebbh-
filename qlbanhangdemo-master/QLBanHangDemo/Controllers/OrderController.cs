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
    public class OrderController : Controller
    {
        private readonly IProductRepository _productRepository;
        private readonly IOrderDetailReponsitory _orderDetailRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly ICustomerReponsitory _customerRepository;
        private readonly IPaymentReponsitory _paymentRepository;


        public OrderController(IProductRepository productRepository, IOrderRepository orderRepository, ICustomerReponsitory customerReponsitory, IOrderDetailReponsitory orderDetailReponsitory, IPaymentReponsitory paymentReponsitory)
        {
            _productRepository = productRepository;
            _customerRepository = customerReponsitory;
            _orderDetailRepository = orderDetailReponsitory;
            _orderRepository = orderRepository;
            _paymentRepository = paymentReponsitory;
        }

        // GET: OrderController
        public IActionResult Index()
        {
            var order = _orderRepository.GetDetailCustomerPaymentByOrderId();
            return View(order);
        }

        // GET: ProductController/Details/5
        public IActionResult Details(int id)
        {
            var orders = _orderRepository.GetDetailCustomerPaymentByOrderId(id);
            if (orders == null)
            {
                return NotFound();
            }

            return View(orders);

        }

        // GET: ProductController/Create
        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.Payment = _paymentRepository.GetAll();
            ///new SelectList(_CaRepository.GetAll().ToList(), "CategoryId", "CategoryName"); ;
            return View();

        }

        // POST: ProductController/Create
        [HttpPost, ActionName("Create")]
        [ValidateAntiForgeryToken]
        public IActionResult CreatePost(Orders orders)
        {
            if (ModelState.IsValid)
            {
                _orderRepository.Add(orders);
            }

            return RedirectToAction("Details", new { Id = orders.OrdersId });

            //  return View();
        }

        public void PaymentList()
        {
            ViewBag.Payment = _paymentRepository.GetAll();
        }
        // GET: ProductController/Edit/5
        public ActionResult Edit(int id)
        {
            var order = _orderRepository.GetDetailCustomerPaymentByOrderId(id);
            if (order == null)
            {
                return NotFound();
            }
            PaymentList();
            return View(order);
        }

        // POST: ProductController/Edit/5

        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public ActionResult EditPost(Orders orders)
        {
            if (ModelState.IsValid)
            {
                _orderRepository.Update(orders);
                return RedirectToAction("Details", new { Id = orders.OrdersId });
            }

            return View("Edit");
        }

        // GET: ProductController/Delete/5
        public IActionResult Delete(int id)
        {
            var order = _orderRepository.GetDetailCustomerPaymentByOrderId(id);
            return View(order);
        }

        // POST: ProductController/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeletePost(int id)
        {
            var orders = _orderRepository.GetById(id);
            if (orders == null)
            {
                return NotFound();
            }

            _orderRepository.Delete(orders);
            return RedirectToAction("Index");
        }
    }
}





