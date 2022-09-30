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
    public class CategoryReponsitory : Repository<Category>, ICategoryReponsitory
    {
        public CategoryReponsitory(QLBanHangMVC2Context BHContext) : base(BHContext)
        {
        }


        public IEnumerable<Product> GetProductsByCategory(int categoryId)
        {
            return QLBanHangMVC2Context.Products
               .Where(e => e.CategoryId == categoryId)
               .Include(x => x.ProductName).Include(x => x.ProductImage)
               .Include(x => x.ProductId).Include(x => x.Category)
               .Include(x => x.ProductPrice).Include(x => x.Brand)
               .ToList();
        }
    }
}
