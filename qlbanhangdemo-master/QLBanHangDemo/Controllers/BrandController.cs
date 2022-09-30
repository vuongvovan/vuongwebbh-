using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QLBanHangDemo.Models;
using QLBanHangDemo.Services.IRepository;
using QLBanHangDemo.Services.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QLBanHangDemo.Controllers
{
    public class BrandController : Controller
    {
        // GET: 
        private readonly IProductRepository _ProductRepository;
        private readonly IBrandRepository _BrandRepository;

        public BrandController(IProductRepository productRepository, IBrandRepository brandRepository)
        {
            _ProductRepository = productRepository;
            _BrandRepository = brandRepository;
        }

        public IActionResult Index()
        {
            var brand = _BrandRepository.GetAll();
            return View(brand);
        }

        public IActionResult Details(int detailId)
        {
            var brand = _BrandRepository.GetById(detailId);
            if (brand == null)
            {
                return NotFound();
            }

            return View(brand);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost, ActionName("Create")]
        [ValidateAntiForgeryToken]
        public IActionResult CreatePost(Brand model)
        {
            if (ModelState.IsValid)
            {
                _BrandRepository.Add(model);
            }

            return RedirectToAction("Details", new { detailId = model.BrandId });
        }
        public IActionResult Edit(int id)
        {
            var brand = _BrandRepository.GetById(id);
            if (brand == null)
            {
                return NotFound();
            }

            return View(brand);
        }

        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public IActionResult EditPost(Brand model)
        {
            if (ModelState.IsValid)
            {
                _BrandRepository.Update(model);
                return RedirectToAction("Details", new { detailId = model.BrandId });
            }

            return View("Edit");
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var brand = _BrandRepository.GetById(id);
            return View(brand);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(int BrandId)
        {
            var brand = _BrandRepository.GetById(BrandId);
            if (brand == null)
            {
                return NotFound();
            }

            _BrandRepository.Delete(brand);
            return RedirectToAction("Index");
        }
        [HttpPost]
        public IActionResult UploadImage(List<IFormFile> formFiles)
        {
            string path = "";
            if (formFiles != null)
            {
                foreach (var formFile in Request.Form.Files)
                {
                    string folder = $"UploadFiles/Images/Products/Description/";
                    path = ImageUtils.UploadFileImage(formFile, folder);
                }
            }
            string filePath = "https://localhost:44324/" + path;
            return Json(new { url = filePath });
        }
    }
}
