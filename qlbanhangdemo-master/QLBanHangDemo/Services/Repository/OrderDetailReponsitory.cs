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
    public class OrderDetailReponsitory : Repository<OrderDetail>, IOrderDetailReponsitory
    {
        public OrderDetailReponsitory(QLBanHangMVC2Context BHContext) : base(BHContext)
        {

        }
        public IEnumerable<OrderDetail> GetOrderDetails(int OrderId)
        {
            return QLBanHangMVC2Context.OrderDetails
               .Where(e => e.OrdersId == OrderId)
               .Include(x => x.ProductName).Include(x => x.Product.ProductImage)
               .Include(x => x.ProductId).Include(x => x.ProductPrice)
               .Include(x => x.ProductSalesQuantity).Include(x => x.Product.Brand.BrandName)
               .ToList();
        }

        public IEnumerable<OrderDetail> GetProductOredrByOrderDetailId()
        {
            return QLBanHangMVC2Context.OrderDetails.Include(x => x.Orders).Include(x => x.Product).ToList();
        }

        public OrderDetail GetProductOredrByOrderDetailId(int id)
        {
            return QLBanHangMVC2Context.OrderDetails.Include(x => x.Orders).Include(x => x.Product).FirstOrDefault(x => x.OrderDetailId == id);
        }
    }
}
