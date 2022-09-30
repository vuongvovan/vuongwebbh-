using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using QLBanHangDemo.Models;
using QLBanHangDemo.Services.IRepository;
using QLBanHangDemo.ViewModels;
using ReflectionIT.Mvc.Paging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SixLabors.ImageSharp.Processing;
using SixLabors.ImageSharp;

namespace QLBanHangDemo.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductRepository _ProductRepository;
        private readonly ICategoryReponsitory _CaRepository;
        private readonly IBrandRepository _BrandRepository;

        public ProductController(IProductRepository productRepository, ICategoryReponsitory categoryReponsitory, IBrandRepository brandRepository)
        {
            _ProductRepository = productRepository;
            _CaRepository = categoryReponsitory;
            _BrandRepository = brandRepository;
        }

        // GET: ProductController
        /*
         public IActionResult Index()
         {
            var products = _ProductRepository.GetBrandCattogoryByProductId();
            return View(products);
         }
        */
        /*
        public IActionResult Index()
        {
            var departments = _ProductRepository.BrandCategoryToProduct();
            return View(departments);
        }
        */
        // GET: ProductController/Details/5

        public  IActionResult Index(int pageindex = 1)
        {
            
            var product = _ProductRepository.GetBrandCattogoryByProductId();
            var model = PagingList.Create(product, 4, pageindex);
            return View(model);
        }

        public IActionResult Details(int id)
        {
            var product = _ProductRepository.GetBrandCattogoryByProductId(id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);

        }

        // GET: ProductController/Create
        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.Brands = _BrandRepository.GetAll();
            ViewBag.Categories = _CaRepository.GetAll();
            ///new SelectList(_CaRepository.GetAll().ToList(), "CategoryId", "CategoryName"); ;
            return View();
           
        }

        // POST: ProductController/Create
        [HttpPost, ActionName("Create")]
        [ValidateAntiForgeryToken]
        public IActionResult CreatePost(Product product, IFormFile formFile)
        {
            if (ModelState.IsValid)
            {
                if (product.ImageFile != null)
                {
                    string folder = $"UploadFiles/Images/Products/{DateTime.Now:yyyyMMdd}/";
                    product.ProductImage = ImageUtils.UploadFileImage(product.ImageFile, folder);
                }
                _ProductRepository.Add(product);
                return RedirectToAction(nameof(Index));
            }     
            return RedirectToAction("Details", new {Id = product.ProductId }); 
        }
        public void CategoryList()
        {
            ViewBag.Category = _CaRepository.GetAll();
        }
        public void BrandList()
        {
            ViewBag.Brand = _BrandRepository.GetAll();
        }
        // GET: ProductController/Edit/5
        public ActionResult Edit(int id)
        {
            var product = _ProductRepository.GetBrandCattogoryByProductId(id);
            if (product == null)
            {
                return NotFound();
            }
            CategoryList();
            BrandList();
            return View(product);
        }

        // POST: ProductController/Edit/5

        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public ActionResult EditPost(Product product)
        {
            if (ModelState.IsValid)
            {
                if (product.ImageFile != null)
                {
                    string folder = $"UploadFiles/Images/Products/{DateTime.Now:yyyyMMdd}/";
                    product.ProductImage = ImageUtils.UploadFileImage(product.ImageFile, folder);
                }
                _ProductRepository.Update(product);
                return RedirectToAction("Details", new { Id = product.ProductId });
            }
            return View("Edit");
        }

        // GET: ProductController/Delete/5
        public IActionResult Delete(int id)
        {
            var product = _ProductRepository.GetBrandCattogoryByProductId(id);
            return View(product);
        }

        // POST: ProductController/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeletePost(int ProductId)
        {
            var product = _ProductRepository.GetById(ProductId);
            if (product == null)
            {
                return NotFound();
            }

            _ProductRepository.Delete(product);
            return RedirectToAction("Index");
        }
        /// <summary>
        /// ////////////////
        /// </summary>
        /// <param name="formFiles"></param>
        /// <returns></returns>
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
    public class ImageUtils
    {
        public static string UploadFileImage(IFormFile image, string folder)
        {

            try
            {

                var filePath = Path.Combine(
                    Directory.GetCurrentDirectory(), "wwwroot",
                    folder);

                bool folderExists = Directory.Exists(filePath);
                if (!folderExists)
                    Directory.CreateDirectory(filePath);

                var url = "";

                if (image.Length > 0)
                {
                    var id = Guid.NewGuid();
                    string name = $"{id}_{image.FileName.Replace(" ", "")}";
                    var fullpath = filePath + name;

                    using (var img = Image.Load(image.OpenReadStream()))
                    {
                        int width = img.Width;
                        if (img.Width > 800)
                        {
                            width = 800;
                        }
                        img.Mutate(x => x
                             .Resize(width, 0)
                         );

                        img.Save(fullpath);

                        url = url + folder + name;
                    }
                }

                return url;
            }
            catch
            {
                throw;
            }
        }

    }

   
}

