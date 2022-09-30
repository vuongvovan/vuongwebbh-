using QLBanHangDemo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QLBanHangDemo.Services.IRepository
{
    public interface IProductRepository : IRepository<Product>
    {
        IEnumerable<Product> GetBrandCattogoryByProductId();
        Product GetBrandCattogoryByProductId(int id);
        IEnumerable<Product> GetProductIndex();

        IEnumerable<Product> GetProductByCateId(int id);
        IEnumerable<Product> GetProductByBrandId(int id);

    }
}
