using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QLBanHangDemo.Services.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using QLBanHangDemo.Models;
using Newtonsoft.Json;
using Microsoft.EntityFrameworkCore;
using QLBanHangDemo.ViewModels;
using Microsoft.AspNetCore.Identity;

namespace QLBanHangDemo.Controllers
{
    public class PageProductController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IProductRepository _ProductRepository;
        private readonly ICategoryReponsitory _CaRepository;
        private readonly IBrandRepository _BrandRepository;
        // GET: PageProductController
        public PageProductController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, IProductRepository productRepository, ICategoryReponsitory categoryReponsitory, IBrandRepository brandRepository)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _ProductRepository = productRepository;
            _CaRepository = categoryReponsitory;
            _BrandRepository = brandRepository;
        }
        public void CategoryListName()
        {
            ViewBag.CategoryName = _CaRepository.GetAll();
        }
        public void BrandListName()
        {
            ViewBag.BrandName = _BrandRepository.GetAll();
        }

        public IActionResult ShowProductDetail(int id)
        {
            LoadCateBrand();
            var productDetail = _ProductRepository.GetById(id);
            return View(productDetail);
        }


        public IActionResult ShowProductBrand(int id)
        {
            LoadCateBrand();
            var productBrand = _ProductRepository.GetProductByBrandId(id);
            return View(productBrand);
        }

        public void LoadCateBrand()
        {
            var ListCateName = _CaRepository.GetAll();
            ViewBag.Cate = ListCateName;
            var ListBrandName = _BrandRepository.GetAll();
            ViewBag.BrandName = ListBrandName;
        }
        public IActionResult ShowProductCate(int id)
        {
            LoadCateBrand();
            var productCate = _ProductRepository.GetProductByCateId(id);
            ViewBag.ProductCate = productCate;
            return View(productCate);
        }

        public IActionResult Index()
        {
            LoadCateBrand();
            var model = _ProductRepository.GetBrandCattogoryByProductId();
            ViewBag.Product = model;
            ViewData["ListProduct"] = model;
            return View(model);
        }

        // Key lưu chuỗi json của Cart
        public const string CARTKEY = "cart";

        // Lấy cart từ Session (danh sách CartItem)
        List<CartItem> GetCartItems()
        {

            var session = HttpContext.Session;
            string jsoncart = session.GetString(CARTKEY);
            if (jsoncart != null)
            {
                return JsonConvert.DeserializeObject<List<CartItem>>(jsoncart);
            }
            return new List<CartItem>();
        }

        // Xóa cart khỏi session
        void ClearCart()
        {
            var session = HttpContext.Session;
            session.Remove(CARTKEY);
        }

        // Lưu Cart (Danh sách CartItem) vào session
        void SaveCartSession(List<CartItem> ls)
        {
            var session = HttpContext.Session;
            string jsoncart = JsonConvert.SerializeObject(ls);
            session.SetString(CARTKEY, jsoncart);
        }
        /// Thêm sản phẩm vào cart
     
        public IActionResult AddToCart( int id)
        {
      
            var product = _ProductRepository.GetById(id);
            if (product == null)
                return NotFound("Không có sản phẩm");

            // Xử lý đưa vào Cart ...
            var cart = GetCartItems();
            var cartitem = cart.Find(p => p.product.ProductId == id);
            if (cartitem != null)
            {
                // Đã tồn tại, tăng thêm 1
                cartitem.quantity++;
            }
            else
            {
                //  Thêm mới
                cart.Add(new CartItem() { quantity = 1, product = product });
            }

            // Lưu cart vào Session
            SaveCartSession(cart);
            // Chuyển đến trang hiện thị Cart
            return RedirectToAction(nameof(Cart));
        }
        // Hiện thị giỏ hàng
        [Route("/cart", Name = "cart")]
        public IActionResult Cart()
        {
            LoadCateBrand();
            return View(GetCartItems());
        }
        [Route("/updatecart", Name = "updatecart")]
        [HttpPost]
        public IActionResult UpdateCart([FromForm] int productid, [FromForm] int quantity)
        {
            // Cập nhật Cart thay đổi số lượng quantity ...
            var cart = GetCartItems();
            var cartitem = cart.Find(p => p.product.ProductId == productid);
            if (cartitem != null)
            {
                // Đã tồn tại, tăng thêm 1
                cartitem.quantity = quantity;
            }
            SaveCartSession(cart);
            // Trả về mã thành công (không có nội dung gì - chỉ để Ajax gọi)
            return Ok();
        }
        public IActionResult RemoveCart( int id)
        {
            var cart = GetCartItems();
            var cartitem = cart.Find(p => p.product.ProductId == id);
            if (cartitem != null)
            {
                // Đã tồn tại, tăng thêm 1
                cart.Remove(cartitem);
            }

            SaveCartSession(cart);
            return RedirectToAction(nameof(Cart));
        }

        [HttpGet]
        public IActionResult Login()
        {
            LoadCateBrand();
            return View();
        }
        [HttpGet]
        public IActionResult Regiter()
        {
            LoadCateBrand();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Regiter(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return NotFound();
            }

            var user = new IdentityUser
            {
                UserName = model.Email,
                Email = model.Email
            };

            var success = await _userManager.CreateAsync(user, model.Password);
            if (success.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, "Customer");
                return RedirectToAction("Login");
            }


            foreach (var error in success.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }

            return View("Regiter");
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return NotFound();
            }

            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                return NotFound();
            }

            var success = await _signInManager.PasswordSignInAsync(user, model.Password, true, false);

            if (success.Succeeded)
            {
                return RedirectToAction("Index");
            }

            return View("Login");
        }


        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index");
        }


        public IActionResult AccessDenied()
        {
            if (_signInManager.IsSignedIn(User))
            {
                return View();
            }
            return RedirectToAction("Login", "Account");

        }
    }
}
