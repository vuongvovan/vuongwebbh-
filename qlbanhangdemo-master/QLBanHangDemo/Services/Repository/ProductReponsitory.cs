using Microsoft.EntityFrameworkCore;
using QLBanHangDemo.DAL;
using QLBanHangDemo.Models;
using QLBanHangDemo.Services.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QLBanHangDemo.Services.Repository
{
    public class ProductReponsitory : Repository<Product>, IProductRepository
    {
        public ProductReponsitory(QLBanHangMVC2Context BHContext) : base(BHContext)
        {

        }
        public IEnumerable<Product> GetBrandCattogoryByProductId()
        {  
              return QLBanHangMVC2Context.Products.Include(x => x.Category).Include(x => x.Brand).ToList();
        }

        public IEnumerable<Product> GetProductByBrandId(int id)
        {
            return QLBanHangMVC2Context.Products
              .Include(x => x.Brand)
               .Include(x => x.Category)
              .Where(x => x.BrandId == id)
              .ToList();
        }

        public IEnumerable<Product> GetProductByCateId(int id)
        {
            return QLBanHangMVC2Context.Products
              .Include(x => x.Category)
               .Include(x => x.Brand)
              .Where(x => x.CategoryId == id)
              .ToList();
        }

        public IEnumerable<Product> GetProductIndex()
        {
            return QLBanHangMVC2Context.Products
               .Include(x => x.ProductName).Include(x => x.ProductImage)
               .Include(x => x.ProductId).Include(x => x.Category.CategoryName)
               .Include(x => x.ProductPrice).Include(x => x.Brand.BrandName)
               .Include(x => x.ProductStatus).Include(x => x.ProductDesc)
               .ToList();
        }

        Product IProductRepository.GetBrandCattogoryByProductId(int id)
        {
            return QLBanHangMVC2Context.Products.Include(x => x.Category).Include(x => x.Brand).FirstOrDefault(x => x.ProductId == id);
        }
    }
}
