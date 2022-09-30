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
    public class IBrandReponsitory : Repository<Brand>, IBrandRepository
    {
        public IBrandReponsitory(QLBanHangMVC2Context BHContext) : base(BHContext)
        {

        }


        public IEnumerable<Product> GetProductsByBrandId(int BrandId)
        {
            return QLBanHangMVC2Context.Products
               .Where(e => e.BrandId == BrandId)
               .Include(x => x.ProductName).Include(x => x.ProductImage)
               .Include(x => x.ProductId).Include(x => x.Category.CategoryName)
               .Include(x => x.ProductPrice).Include(x => x.Brand.BrandName)
               .ToList();
        }
    }
}
