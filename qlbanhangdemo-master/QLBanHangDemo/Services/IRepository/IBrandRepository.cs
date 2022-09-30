
using QLBanHangDemo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QLBanHangDemo.Services.IRepository
{
    public interface IBrandRepository : IRepository<Brand>
    {
        IEnumerable<Product> GetProductsByBrandId(int BrandId);
    }
}
