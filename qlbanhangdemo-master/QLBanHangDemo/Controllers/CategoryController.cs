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
    public class CategoryController : Controller
    {
        // GET: CategoryController
        private readonly IProductRepository _productRepository;
        private readonly ICategoryReponsitory _categoryRepository;

        public CategoryController(IProductRepository productRepository, ICategoryReponsitory categoryRepository)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
        }

        public IActionResult Index()
        {
            var category = _categoryRepository.GetAll();
            return View(category);
        }

        public IActionResult Details(int detailId)
        {
            var category = _categoryRepository.GetById(detailId);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost, ActionName("Create")]
        [ValidateAntiForgeryToken]
        public IActionResult CreatePost(Category model)
        {
            if (ModelState.IsValid)
            {
                _categoryRepository.Add(model);
            }

            return RedirectToAction("Details", new { detailId = model.CategoryId });
        }

        public IActionResult Edit(int id)
        {
            var category = _categoryRepository.GetById(id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public IActionResult EditPost(Category model)
        {
            if (ModelState.IsValid)
            {
                _categoryRepository.Update(model);
                return RedirectToAction("Details", new { detailId = model.CategoryId });
            }

            return View("Edit");
        }
        //
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var category = _categoryRepository.GetById(id);
            return View(category);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(int CategoryId)
        {
            var category = _categoryRepository.GetById(CategoryId);
            if (category == null)
            {
                return NotFound();
            }

            _categoryRepository.Delete(category);
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
